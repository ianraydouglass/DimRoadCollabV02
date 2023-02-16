using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public enum positionState
{
	Stand,
	Crouch,
	Crawl
}

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class FirstPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 6.0f;
		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.1f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90.0f;

		//added by Ian on 122922
		public InventoryManager inventoryManager;

		//added by Ian on 070722
		public float maxRayDist = 3f;
		public bool canInteract = true;

		//added by Ian on 070822
		public GameObject hoistPosition;
		public GameObject stowPosition;
		public GameObject hoistedObject;
		public GameObject stowedObject;
		public CubeSpaceTest hoistTest;
		public CubeSpaceTest stowTest;

		private Vector3 hoistedObjectOffset;
		private Vector3 stowedObjectOffset;

		public bool handsEmpty = true;
		public GameObject interactionTarget;
		//added by Ian on 070922
		public GameEvent forbiddenAction;
		public float pullSpeed = 5f;
		public float pullDistance = 0.5f;

		//added by Ian on 071222
		public float knockbackAngleY = 0.2f;
		public float knockbackSpeed = 6f;
		private bool knockbackInMotion = false;
		private Vector3 knockbackAngleFull;
		public float knockTime = 0.3f;
		public float standingHeight = 3.4f;
		public float crouchingHeight = 1.9f;
		public float crawlingHeight = 0.85f;
		
		public positionState playerPositionState = positionState.Stand;
		public float standingCameraPos = 2.2f;
		public float crouchingCameraPos = 1.23f;
		public float crawlingCameraPos = 0.55f;
		public float standingGroundedOffset = 0.3f;
		public float croucingGroundedOffset = -0.4f;
		public float crawlingGroundedOffset = -1f;
		public float standingScale = 1.7f;
		public float crouchingScale = 0.95f;
		public float crawlingScale = 0.425f;
		public GameObject capsuleObject;
		public float crouchSpeed = 3.5f;
		public float crawlSpeed = 2f;
		//added by Ian on 072922
		[Space(10)]
		public bool isTesting;
		private bool toolTimerRunning;
		public GameEvent breakEvent;
		public HudActionSender actionSender;
		[Space(10)]
		public EventSystem eventSystem;
		public GameEvent pauseEvent;
		public GameEvent inventoryEvent;
		[Space(10)]
		public ToolUseManager toolManager;
		private Coroutine toolTime;
		[Space(10)]
		public HUDHandler hudHandler;

		// cinemachine
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

	
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		private PlayerInput _playerInput;
#endif
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;

		private const float _threshold = 0.01f;

		private bool IsCurrentDeviceMouse
		{
			get
			{
				#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
				return _playerInput.currentControlScheme == "KeyboardMouse";
				#else
				return false;
				#endif
			}
		}

		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
			_playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;
			actionSender = GetComponent<HudActionSender>();
			inventoryManager.hoistObject = hoistPosition;
			inventoryManager.hoistTest = hoistTest;
			HearMenuClose();
		}

		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			if (knockbackInMotion == false)
            {
				Move();
            }
			else
            {
				KnockBackMove();
            }
			//added by Ian D. on 070722
			//modified by Ian D. on 070822 to see if hands are full of a hoisted cube
			if (canInteract == true && hoistedObject == null)
            {
				CheckRange();
			}
			
			
		}

		private void FixedUpdate()
        {
			if (hoistedObject != null)
			{
				HoldHandler(hoistPosition, hoistedObjectOffset, hoistedObject);
			}
			if (stowedObject != null)
			{
				HoldHandler(stowPosition, stowedObjectOffset, stowedObject);
			}
		}

		private void LateUpdate()
		{
			CameraRotation();
		}

		//added by Ian D. on 070722
		//starting by drawing a ray that detects if the target is interactable and pints debug if so, for test
		private void CheckRange()
        {
			//using the cinemachine camera target becauses its pointing where you are pointing
			Ray ray = new Ray(CinemachineCameraTarget.transform.position, CinemachineCameraTarget.transform.forward);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, maxRayDist))
            {
				if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Interactable") || hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Sifted"))
				{
					Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
					//this prevents the message from being spammed constantly
					if (interactionTarget != hitInfo.transform.gameObject)
                    {
						interactionTarget = hitInfo.transform.gameObject;
						DisplayTarget(hitInfo.transform.gameObject);
						//print("Would you like to interact with " + hitInfo.transform.name + "?");
						CancelUseTool();
						
					}
					
					
				}
				//we want to clear the target reference if the ray hits something non-interactable
				else
                {
					interactionTarget = null;
					//hud handler displays prompts for interactables
					//we clear it when the target is not valid
					hudHandler.NoTarget();
					CancelUseTool();
				}

			}
			//we also want to clear the target reference if the ray hits nothign at all
			else
			{
				interactionTarget = null;
				hudHandler.NoTarget();
				CancelUseTool();
			}
		}
		//added on 2-16-2023
		void DisplayTarget(GameObject targetObject)
        {

			Interactable i = targetObject.GetComponent<Interactable>();
			if (i == null)
            {
				hudHandler.NoTarget();
				return;
            }
			//check if there is a tool in your hands
			else
            {
				hudHandler.TargetObject(i.inType, i.displayName);
            }
        }

		//added by Ian D. On 070922
		void HoldHandler(GameObject holder, Vector3 offset, GameObject cube)
        {
			if ((cube.transform.position + offset) != holder.transform.position)
            {
				float dist = Vector3.Distance((cube.transform.position + offset), holder.transform.position);
				if (dist >= pullDistance)
                {
					if (holder == hoistPosition)
                    {
						DropHeld();
                    }
					if (holder == stowPosition)
                    {
						DropStowed();
                    }
                }
				else
                {
					Rigidbody body = cube.GetComponent<Rigidbody>();
					Vector3 movePosition = (cube.transform.position + offset);
					//cube.transform.position = Vector3.MoveTowards(cube.transform.position, holder.transform.position, pullSpeed * Time.deltaTime);
					movePosition = Vector3.MoveTowards((cube.transform.position + offset), holder.transform.position, pullSpeed * Time.deltaTime);
					body.MovePosition(movePosition);
				}
				
            }
        }
		public void DropHeld()
        {
			hoistedObject.transform.parent = null;
			hoistedObject.GetComponent<Interactable>().DropThis();
			hoistedObject = null;
			canInteract = true;
			handsEmpty = true;
			Debug.Log("forced to drop object from hands");
			
			if (playerPositionState == positionState.Crouch)
            {
				actionSender.crouchingDropFront.Raise();
			}
			else if (playerPositionState == positionState.Crawl)
            {
				actionSender.crawlingDropFront.Raise();
			}
			else
            {
				//action calls for standing
				actionSender.standingDropFront.Raise();
            }
		}
		public void DropStowed()
        {
			stowedObject.transform.parent = null;
			stowedObject.GetComponent<Interactable>().DropThis();
			stowedObject = null;
			Debug.Log("forced to drop object from back");
			if (playerPositionState == positionState.Crouch)
			{
				actionSender.crouchingDropBack.Raise();
			}
			else if (playerPositionState == positionState.Crawl)
			{
				actionSender.crawlingDropBack.Raise();
			}
			else
			{
				//action calls for standing
				actionSender.standingDropBack.Raise();
			}
		}

		//set this up so it checks if the target object is null or not. if not, and it can be lifted, and your hands are empty, you lift
		public void OnHoist()
        {
			print("hoist pressed");
			if (interactionTarget != null && hoistedObject == null)
            {
				Interactable handler = interactionTarget.GetComponent<Interactable>();
				if (handler == null)
                {
					Debug.Log("tried to interact with an object that doesn't have the Interactable script attached");
					forbiddenAction.Raise();
					return;
                }
				if (handler.isCube == true)
                {
					canInteract = false;
					handsEmpty = false;
					handler.LiftThis();
					//Encountering problems with new cube on 080122, it is getting offset. trying to set parent later to see if it fixes
					//interactionTarget.transform.parent = hoistPosition.transform;
					hoistedObjectOffset = handler.offsetValue;
					interactionTarget.transform.position = hoistPosition.transform.position + handler.offsetValue;
					interactionTarget.transform.rotation = hoistPosition.transform.rotation;
					interactionTarget.transform.parent = hoistPosition.transform;
					hoistedObject = interactionTarget;
					interactionTarget = null;
					hudHandler.NoTarget();
					if (playerPositionState == positionState.Crouch)
					{
						actionSender.crouchingGrab.Raise();
					}
					else if (playerPositionState == positionState.Crawl)
					{
						actionSender.crawlingGrab.Raise();
					}
					else
					{
						//action calls for standing
						actionSender.standingGrab.Raise();
					}

				}
				else
                {
					Debug.Log("target object is not a cube and cannot be hoisted");
					forbiddenAction.Raise();
                }
            }
			else if (hoistedObject != null)
            {
				Interactable handler = hoistedObject.GetComponent<Interactable>();
				if (handler.isCube == true)
                {
					hoistedObject.transform.parent = null;
					handler.DropThis();
					hoistedObject = null;
					canInteract = true;
					handsEmpty = true;
					if (playerPositionState == positionState.Crouch)
					{
						actionSender.crouchingDropFront.Raise();
					}
					else if (playerPositionState == positionState.Crawl)
					{
						actionSender.crawlingDropFront.Raise();
					}
					else
					{
						//action calls for standing
						actionSender.standingDropFront.Raise();
					}
				}
			}
        }

		public void OnStow()
        {
			print("stow pressed");
			if (stowedObject == null)
            {
				if (hoistedObject != null)
                {
					stowedObject = hoistedObject;
					Interactable handler = hoistedObject.GetComponent<Interactable>();
					//hoistedObject.transform.parent = stowPosition.transform;
					stowedObjectOffset = handler.offsetValue;
					hoistedObject.transform.position = stowPosition.transform.position + handler.offsetValue;
					hoistedObject.transform.rotation = stowPosition.transform.rotation;
					hoistedObject.transform.parent = stowPosition.transform;
					hoistedObject = null;
					canInteract = true;
					handsEmpty = true;
					if (playerPositionState == positionState.Crouch)
					{
						actionSender.crouchingStow.Raise();
					}
					else if (playerPositionState == positionState.Crawl)
					{
						actionSender.crawlingStow.Raise();
					}
					else
					{
						//action calls for standing
						actionSender.standingStow.Raise();
					}

					//stow the held object on your back
				}
				else
                {
					print("no object to stow");
					forbiddenAction.Raise();
				}
            }
			else
            {
				if (hoistedObject == null && canInteract == true && handsEmpty == true)
                {
					canInteract = false;
					handsEmpty = false;
					hoistedObject = stowedObject;
					stowedObject.transform.parent = hoistPosition.transform;
					stowedObject.transform.position = hoistPosition.transform.position;
					stowedObject.transform.rotation = hoistPosition.transform.rotation;
					stowedObject = null;
					hudHandler.NoTarget();
					if (playerPositionState == positionState.Crouch)
					{
						actionSender.crouchingUnStow.Raise();
					}
					else if (playerPositionState == positionState.Crawl)
					{
						actionSender.crawlingUnStow.Raise();
					}
					else
					{
						//action calls for standing
						actionSender.standingUnStow.Raise();
					}

					//hold the hoisted object
				}
				else
                {
					print("no room for this object on your back");
					forbiddenAction.Raise();
				}
				
			}				
        }

		public bool TestHoistRoom()
        {
			bool hasSpace = true;
			return hasSpace;
        }

		public bool TestStowRoom()
        {
			bool hasSpace = true;
			return hasSpace;
		}

		public void CancelUseTool()
        {
			if(!toolTimerRunning)
            {
				return;
            }
			Debug.Log("toolUseCanceled");
			toolTimerRunning = false;
			StopCoroutine(toolTime);
			toolManager.CancelToolUse();
			//do visual thing for cancel use

		}

		public void ConfirmUseTool()
        {
			if(!interactionTarget)
            {
				return;
            }
			toolManager.FinishToolUse(interactionTarget);
			toolTimerRunning = false;
		}

		public void OnWheelScroll(InputValue value)
        {
			Vector2 scroll = value.Get<Vector2>();
			
			if (scroll.y > 0.01)
            {
				Debug.Log("Scroll up on Y");
				toolManager.CycleToolPositive();
				if(toolManager.HasTool())
                {
					
					handsEmpty = false;
                }
				else
                {
					
					handsEmpty = true;
                }
            }
			if (scroll.y < -0.01)
			{
				Debug.Log("Scroll down on Y");
				toolManager.CycleToolNegative();
				if (toolManager.HasTool())
				{
					
					handsEmpty = false;
				}
				else
				{
					
					handsEmpty = true;
				}
			}


		}

		
		//added by Ian D. on 071122
		public void OnUseR()
        {
			print("UseR pressed");
			if (interactionTarget != null && handsEmpty == true)
            {
				Interactable handler = interactionTarget.GetComponent<Interactable>();
				if (handler == null)
				{
					Debug.Log("tried to sift an object that doesn't have the Interactable script attached");
					forbiddenAction.Raise();
					return;
				}
				if (handler.isDebris == true && handler.isSifted == false)
				{
					//should disable parts of the debris and instantiate the new stuff
					handler.SiftThis();
					

				}
				else if (handler.isDebris == true && handler.isSifted == true)
                {
					//pack method here
					handler.PackThis();
					Debug.Log("trying to pack debris");
                }
			}
			if (interactionTarget != null && toolManager.HasTool())
            {
				if (toolManager.CanUseTool(interactionTarget))
                {
					toolTimerRunning = true;
					toolTime = StartCoroutine("ToolUseTimer", toolManager.CurrentToolTime());
					//start tool timer
                }
            }
		}
		IEnumerator ToolUseTimer(int useTime)
		{
			yield return new WaitForSeconds(useTime);
			ConfirmUseTool();
		}

		public void OnInteractR()
        {
			print("intaractR pressed");
			if (interactionTarget != null && hoistedObject == null && handsEmpty == true)
            {
				Interactable handler = interactionTarget.GetComponent<Interactable>();
				if (handler == null)
				{
					Debug.Log("tried to interact with an object that doesn't have the Interactable script attached");
					forbiddenAction.Raise();
					return;
				}
				if (handler.isButton == true)
                {
					handler.SimpleInteract();
				}
				if (handler.isDebris == true)
                {
					//they have to hold press to break it or pack it
					return;
                }
				if (handler.isItem == true)
                {
					print("add item to inventory");
					if (inventoryManager)
                    {
						inventoryManager.AddToInventory(interactionTarget.GetComponent<ItemObject>().gameItem);
					}
					handler.AddToInventory();
				}
				if (handler.isTool == true)
				{
					
					if (toolManager)
					{
						toolManager.PickupTool(interactionTarget.GetComponent<ToolObject>().toolItem);
					}
					handler.AddToInventory();
				}
				else
                {
					forbiddenAction.Raise();
				}

			}
		}

		public void OnInteractL()
        {
			if (interactionTarget != null && hoistedObject == null && handsEmpty == true)
            {
				Interactable handler = interactionTarget.GetComponent<Interactable>();
				if (handler == null)
				{
					Debug.Log("tried to interact with an object that doesn't have the Interactable script attached");
					forbiddenAction.Raise();
					return;
				}
				if (handler.isItem == true)
				{
					print("add item to inventory");
					if (inventoryManager)
					{
						inventoryManager.AddToInventory(interactionTarget.GetComponent<ItemObject>().gameItem);
					}
					handler.AddToInventory();
				}

			}
			
			print("intaractL pressed");
		}

		public void OpenItemRequired()
        {

        }

		//added by Ian D. on 071222
		public void OnGoDown()
        {
			if (playerPositionState == positionState.Stand)
            {
				playerPositionState = positionState.Crouch;
				_controller.height = crouchingHeight;
				capsuleObject.transform.localScale = new Vector3(.85f, crouchingScale, .85f);
				GroundedOffset = croucingGroundedOffset;
				Vector3 newPosition = CinemachineCameraTarget.transform.localPosition;
				newPosition.y = crouchingCameraPos;
				CinemachineCameraTarget.transform.localPosition = newPosition;
				actionSender.downToCrouchEvent.Raise();
            }
			else if (playerPositionState == positionState.Crouch)
			{
				playerPositionState = positionState.Crawl;
				_controller.height = crawlingHeight;
				capsuleObject.transform.localScale = new Vector3(.85f, crawlingScale, .85f);
				GroundedOffset = crawlingGroundedOffset;
				Vector3 newPosition = CinemachineCameraTarget.transform.localPosition;
				newPosition.y = crawlingCameraPos;
				CinemachineCameraTarget.transform.localPosition = newPosition;
				actionSender.downToCrawlEvent.Raise();
			}
			else if (playerPositionState == positionState.Crawl)
			{
				forbiddenAction.Raise();
			}
		}

		public void OnGoUp()
        {
			if (playerPositionState == positionState.Stand)
			{
				forbiddenAction.Raise();
			}
			else if (playerPositionState == positionState.Crouch)
			{
				playerPositionState = positionState.Stand;
				_controller.height = standingHeight;
				capsuleObject.transform.localScale = new Vector3(.85f, standingScale, .85f);
				GroundedOffset = standingGroundedOffset;
				Vector3 newPosition = CinemachineCameraTarget.transform.localPosition;
				newPosition.y = standingCameraPos;
				CinemachineCameraTarget.transform.localPosition = newPosition;
				actionSender.upToStandEvent.Raise();
			}
			else if (playerPositionState == positionState.Crawl)
			{
				playerPositionState = positionState.Crouch;
				_controller.height = crouchingHeight;
				capsuleObject.transform.localScale = new Vector3(.85f, crouchingScale, .85f);
				GroundedOffset = croucingGroundedOffset;
				Vector3 newPosition = CinemachineCameraTarget.transform.localPosition;
				newPosition.y = crouchingCameraPos;
				CinemachineCameraTarget.transform.localPosition = newPosition;
				actionSender.upToCrouchEvent.Raise();
			}
		}

		//added by Ian D. on 072922
		public void OnBreakTest()
        {
			if (isTesting == true)
            {
				breakEvent.Raise();
            }
        }

		public void OnDropTest()
        {
			if (inventoryManager)
            {
				inventoryManager.DropItem(hoistPosition.transform.position);
            }
        }

		public void OnPause()
        {
			HearMenuOpen();
			pauseEvent.Raise();
		}

		public void HearUnPause()
        {
			HearMenuClose();
		}

		public void HearMenuOpen()
        {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		public void HearMenuClose()
        {
			/*
			GameObject eventObject = GameObject.Find("UI_EventSystem");
			eventSystem = eventObject.GetComponent<EventSystem>();
			EventSystem.current.SetSelectedGameObject(null);*/
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		public void OnInventory()
        {
			HearMenuOpen();
			inventoryEvent.Raise();
        }

		
		//added by Ian D. on 071222
		public void KnockBack(GameObject source)
        {
			if (knockbackInMotion == false)
            {
				knockbackInMotion = true;
				Debug.Log("detected a collision on player at speed from " + source);
				Vector3 pushDirection = this.transform.position - source.transform.position;
				//I want to override the y component so I send the player at an upwards angle
				pushDirection.y = knockbackAngleY;
				knockbackAngleFull = pushDirection;
				//this should launch the player up?
				//_input.jump = true;
				StartCoroutine(KnockBackTimer(knockTime));

			}
			else
            {
				Debug.Log("already in knockback, cannot take push from " + source);

            }
			
		}

		private IEnumerator KnockBackTimer(float knockbackTime)
		{
			yield return new WaitForSeconds(knockbackTime);
			knockbackInMotion = false;
		}


		private void KnockBackMove()
		{
			//trying to hack the movement script for knockback
			//at a future date, include some easing and some transition to normal control afterwards.

			// normalise input direction
			Vector3 inputDirection = new Vector3(knockbackAngleFull.x, 0.0f, knockbackAngleFull.z).normalized;

			

			// move the player
			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
			if (_input.jump == false)
            {
				//knockbackInMotion = false;
            }
		}



		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			// if there is an input
			if (_input.look.sqrMagnitude >= _threshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
				
				_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * _rotationVelocity);
			}
		}

		private void Move()
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			//modified by Ian D. on 071222 for state based speeds
			float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
			if (playerPositionState == positionState.Stand)
            {
				targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
			}
			else if (playerPositionState == positionState.Crouch)
            {
				targetSpeed = crouchSpeed;
            }
			else if (playerPositionState == positionState.Crawl)
            {
				targetSpeed = crawlSpeed;
            }
			else
            {
				targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
			}
			

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				// move
				inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
			}

			// move the player
			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}

		//modified by Ian D. on 071222 to make it so you can only jump while standing
		private void JumpAndGravity()
		{
			
			if (Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (_input.jump && _jumpTimeoutDelta <= 0.0f && playerPositionState == positionState.Stand)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}

				// if we are not grounded, do not jump
				_input.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
	}
}