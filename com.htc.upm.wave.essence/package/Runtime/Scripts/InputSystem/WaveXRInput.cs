// GENERATED AUTOMATICALLY FROM 'Packages/com.htc.upm.wave.essence/Runtime/Scripts/InputSystem/WaveXRInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Wave.Essence.HIDPlugin
{
    public class @WaveXRInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @WaveXRInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""WaveXRInput"",
    ""maps"": [
        {
            ""name"": ""Left"",
            ""id"": ""47e8abb4-d335-4955-ad96-99f0d9cd7991"",
            ""actions"": [
                {
                    ""name"": ""SystemPress"",
                    ""type"": ""Button"",
                    ""id"": ""27f88867-2e5b-481f-96fb-f3a314dcf1d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuPress"",
                    ""type"": ""Button"",
                    ""id"": ""ff99a355-9d66-4a1b-bb77-7520266a5571"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GripPress"",
                    ""type"": ""Button"",
                    ""id"": ""3ab7319c-3430-45b8-8e84-5347e2a6f30a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GripTouch"",
                    ""type"": ""Button"",
                    ""id"": ""9ae035be-fcdc-47fa-a510-018c09f991c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GripAxis"",
                    ""type"": ""Value"",
                    ""id"": ""282f7fc2-5ff8-436a-8f94-f2519488544f"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VolumeUpPress"",
                    ""type"": ""Button"",
                    ""id"": ""75754af3-2bc6-43bd-b7a0-49108a279f4a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VolumeDownPress"",
                    ""type"": ""Button"",
                    ""id"": ""c7c155b4-5665-491f-821c-dc7fedaa9113"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonAPress"",
                    ""type"": ""Button"",
                    ""id"": ""1621dc2f-0936-48e8-9573-a2a583c1c884"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonATouch"",
                    ""type"": ""Button"",
                    ""id"": ""01a11fad-b150-47d9-95aa-995589bfd8b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonBPress"",
                    ""type"": ""Button"",
                    ""id"": ""cd60e68d-9b5c-4806-9695-af4a8d7695e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonBTouch"",
                    ""type"": ""Button"",
                    ""id"": ""cc196255-fdb2-469f-97d8-c3ddc000bade"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonXPress"",
                    ""type"": ""Button"",
                    ""id"": ""a203357d-5043-46c2-a3a5-ad2a9eaec12f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonXTouch"",
                    ""type"": ""Button"",
                    ""id"": ""59b26c2a-da4f-408c-b6ec-7927e4075133"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonYPress"",
                    ""type"": ""Button"",
                    ""id"": ""f8b3b53e-3efe-4c13-b104-bdba95de3e2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonYTouch"",
                    ""type"": ""Button"",
                    ""id"": ""635f1aa9-f054-4f49-a3f3-814b824e07ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BackPress"",
                    ""type"": ""Button"",
                    ""id"": ""b42b9266-469f-47a6-8e19-20b33efa7870"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EnterPress"",
                    ""type"": ""Button"",
                    ""id"": ""7e7fbbf3-d9a6-480e-bbec-b48bc5125f9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchpadPress"",
                    ""type"": ""Button"",
                    ""id"": ""b744a632-2e21-494a-90b2-72a8031d5225"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchpadTouch"",
                    ""type"": ""Button"",
                    ""id"": ""68b42eba-68db-406a-ba80-750810560cd6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchpadAxis"",
                    ""type"": ""Value"",
                    ""id"": ""bbec359c-c56a-4a88-8c04-216c21dcb060"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerPress"",
                    ""type"": ""Button"",
                    ""id"": ""a4f7d607-bd54-45b4-b527-4fa3a001641a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerTouch"",
                    ""type"": ""Button"",
                    ""id"": ""ddb22554-8d99-42ab-b862-11b8849bd628"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerAxis"",
                    ""type"": ""Value"",
                    ""id"": ""79944e80-e534-4eb7-8bd4-3454f4809e9e"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoystickPress"",
                    ""type"": ""Button"",
                    ""id"": ""b8378da2-4613-4362-ba7b-66e7cb0305a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoystickTouch"",
                    ""type"": ""Button"",
                    ""id"": ""29af0f6b-b0c9-478e-8351-09d9649fe743"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoystickAxis"",
                    ""type"": ""Value"",
                    ""id"": ""bb6202c0-7ebe-4b7a-8333-55d0f2e748c9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ParkingTouch"",
                    ""type"": ""Button"",
                    ""id"": ""d9915f9d-d0c5-48aa-8db2-47909c21cd83"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""95372dd6-f15c-4c54-b59a-7c45a50e92b2"",
                    ""path"": ""<WaveXRController>/leftMenuHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""MenuPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""256ad92c-33a2-4be5-939a-cc81e5fd74d7"",
                    ""path"": ""<WaveXRController>/leftGripHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""GripPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2e44ec7-e3a0-4ba3-88b4-f1e5e38975ae"",
                    ""path"": ""<WaveXRController>/leftSystemHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""SystemPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""810499d9-35a9-4ebe-b446-fa986147bad4"",
                    ""path"": ""<WaveXRController>/leftVolumeUpHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""VolumeUpPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c541989-6662-4f13-88fe-ed4bd8fc16d8"",
                    ""path"": ""<WaveXRController>/leftVolumeDownHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""VolumeDownPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48234016-6c57-4ffb-bb1b-943cb8a22cb9"",
                    ""path"": ""<WaveXRController>/leftAHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonAPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0224059b-1ec7-4b44-9e72-b0d155ec9373"",
                    ""path"": ""<WaveXRController>/leftBHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonBPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6ebba7a-180c-4a02-af59-da94aa12a0c2"",
                    ""path"": ""<WaveXRController>/leftXHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonXPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac5b8b3c-1669-4010-90ab-3fa3988da0d5"",
                    ""path"": ""<WaveXRController>/leftYHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonYPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""744f25f3-3fcc-4e04-8fb9-52973865371d"",
                    ""path"": ""<WaveXRController>/leftBackHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""BackPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ffc0706-335b-40dd-b674-efd69bd4d32f"",
                    ""path"": ""<WaveXRController>/leftEnterHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""EnterPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9ec9081-579d-46e4-8e95-c9ff7930fe9f"",
                    ""path"": ""<WaveXRController>/leftTouchpadHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TouchpadPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61dfcf04-96a5-433a-8cf4-2f3c89372118"",
                    ""path"": ""<WaveXRController>/leftBumperHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16480eaf-dac7-4c65-bd9e-bf904ec06b5a"",
                    ""path"": ""<WaveXRController>/leftTriggerHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec53ea6d-1621-4d75-8a98-e19208dc7864"",
                    ""path"": ""<WaveXRController>/leftJoystickHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""JoystickPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0054d484-c05a-42c9-b632-c70e72b50ecb"",
                    ""path"": ""<WaveXRController>/leftJoystickTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""JoystickTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""717bca10-b401-420b-8283-dcbe499efbd6"",
                    ""path"": ""<WaveXRController>/leftParkingTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ParkingTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb1e58d1-8e35-4ef9-96f8-af137892b8a8"",
                    ""path"": ""<WaveXRController>/leftGripTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""GripTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f91ccd0b-ac22-4bbf-9544-4b782d1944cb"",
                    ""path"": ""<WaveXRController>/leftATouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonATouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba7c0f5c-2e26-460c-83b2-8324690f3cbe"",
                    ""path"": ""<WaveXRController>/leftBTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonBTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3bfa02f-9dc1-4ae4-a650-42d49982d9a9"",
                    ""path"": ""<WaveXRController>/leftXTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonXTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acdd6eda-50c7-442c-b241-808df67df37d"",
                    ""path"": ""<WaveXRController>/leftYTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonYTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e9e2e57-cd79-4d5c-ba9b-47941159b4ac"",
                    ""path"": ""<WaveXRController>/leftTouchpadTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TouchpadTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e40d6f75-77b1-46d7-a4be-123d943b13d9"",
                    ""path"": ""<WaveXRController>/leftBumperTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""361c8766-c273-43a9-9fb9-d09702310ef2"",
                    ""path"": ""<WaveXRController>/leftTriggerTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bed7d0d5-befe-4a34-9ee2-dae3d7e5e2f7"",
                    ""path"": ""<WaveXRController>/leftTouchpadAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TouchpadAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba9b709b-50d0-4b7d-80b4-67ae339c8bdf"",
                    ""path"": ""<WaveXRController>/leftBumperAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7cf29841-259a-490a-8e29-ac9df57e93ed"",
                    ""path"": ""<WaveXRController>/leftTriggerAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0328946d-19a4-4f15-bf30-1b4f35e388c4"",
                    ""path"": ""<WaveXRController>/leftJoystickAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""JoystickAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64ac8f5d-48ff-4cd6-9e74-4fc6fcbbcfca"",
                    ""path"": ""<WaveXRController>/leftGripAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""GripAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Right"",
            ""id"": ""bf6ac05b-15d2-4d76-a6b1-05d644ef6065"",
            ""actions"": [
                {
                    ""name"": ""SystemPress"",
                    ""type"": ""Button"",
                    ""id"": ""6fc47fb2-4ae3-44f9-9166-0e14976fa3c2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuPress"",
                    ""type"": ""Button"",
                    ""id"": ""e8bd5f97-8a98-4e1f-bc7c-ad4906b20c56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GripPress"",
                    ""type"": ""Button"",
                    ""id"": ""eaad6e4b-3011-46b3-8d16-fb755c8c0b68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GripTouch"",
                    ""type"": ""Button"",
                    ""id"": ""a274b6d5-8ab6-4eda-bfea-341fc5783f26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GripAxis"",
                    ""type"": ""Value"",
                    ""id"": ""ca39c6e3-240a-4dd1-8d9e-6eb644c9e277"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VolumeUpPress"",
                    ""type"": ""Button"",
                    ""id"": ""393d213d-4a15-4f26-afb3-3a1859689889"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VolumeDownPress"",
                    ""type"": ""Button"",
                    ""id"": ""77153519-522a-44a8-abfd-63e4cd4a2abe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonAPress"",
                    ""type"": ""Button"",
                    ""id"": ""a8282910-46f7-48e2-a47f-ba0ebd27d5a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonATouch"",
                    ""type"": ""Button"",
                    ""id"": ""da3a7520-01ff-4bdc-b6ed-5b1d2d2d787b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonBPress"",
                    ""type"": ""Button"",
                    ""id"": ""dd8d382b-7b93-4e84-ad6c-043b7d36eb2c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonBTouch"",
                    ""type"": ""Button"",
                    ""id"": ""aa66f89e-d159-415f-985c-e359aad74b22"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonXPress"",
                    ""type"": ""Button"",
                    ""id"": ""4dd93046-4b99-49fb-8e95-5fc42164b0bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonXTouch"",
                    ""type"": ""Button"",
                    ""id"": ""8c7c2492-152c-4f8d-9a87-81d74d324a1d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonYPress"",
                    ""type"": ""Button"",
                    ""id"": ""d201ef18-496f-41c8-b8e9-1a75fc32b02e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonYTouch"",
                    ""type"": ""Button"",
                    ""id"": ""3c23d6b5-2b9f-4333-a81d-d018705ba3a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BackPress"",
                    ""type"": ""Button"",
                    ""id"": ""f08aeb9f-aab9-4a48-95f9-242c91e342a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EnterPress"",
                    ""type"": ""Button"",
                    ""id"": ""f8a76034-1eee-45a4-864c-4d724bbfaf0b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchpadPress"",
                    ""type"": ""Button"",
                    ""id"": ""63944ff0-34dc-4d18-b83d-5e72ff087298"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchpadTouch"",
                    ""type"": ""Button"",
                    ""id"": ""312894d4-445d-4005-85d9-c6152b270db7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchpadAxis"",
                    ""type"": ""Value"",
                    ""id"": ""be01d748-453d-44ab-88e9-b983ace9d5af"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerPress"",
                    ""type"": ""Button"",
                    ""id"": ""a9f12d5e-1a1a-40f9-9dcb-0084d9f9c691"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerTouch"",
                    ""type"": ""Button"",
                    ""id"": ""3630803b-13a7-4bc3-94dd-1bb37d580b74"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerAxis"",
                    ""type"": ""Value"",
                    ""id"": ""f72f5400-e98f-41ef-a57b-1735b9c46d66"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoystickPress"",
                    ""type"": ""Button"",
                    ""id"": ""82ba630f-7750-40e8-b1ff-df62ba3ce6a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoystickTouch"",
                    ""type"": ""Button"",
                    ""id"": ""1a55cb4d-f7bc-41e1-b885-968cf1082cb2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoystickAxis"",
                    ""type"": ""Value"",
                    ""id"": ""b7cda155-efa1-4f4c-8723-5cf176e68716"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ParkingTouch"",
                    ""type"": ""Button"",
                    ""id"": ""e36ede07-0217-445e-8095-1c226ccdb904"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""847e2607-633f-42f2-8761-f0a4d9f68ad1"",
                    ""path"": ""<WaveXRController>/rightMenuHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""MenuPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fd54f05-f518-4e43-8311-4ce520be0aa2"",
                    ""path"": ""<WaveXRController>/rightGripHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""GripPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a6b806d-8a59-4510-b578-313f31e5d4e7"",
                    ""path"": ""<WaveXRController>/rightSystemHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""SystemPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d40de3ee-8f96-4601-aec4-564c7b9be56a"",
                    ""path"": ""<WaveXRController>/rightVolumeUpHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""VolumeUpPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8202c52e-cd83-493d-944c-ba6d89a8f0fa"",
                    ""path"": ""<WaveXRController>/rightVolumeDownHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""VolumeDownPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71924507-7ef3-4f39-b41e-115bf75f643a"",
                    ""path"": ""<WaveXRController>/rightAHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonAPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6aed6e0-4b28-427c-a159-0ca80d75f7bd"",
                    ""path"": ""<WaveXRController>/rightBHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonBPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""708c74b4-d7dc-4e28-b100-cabfa75ee69e"",
                    ""path"": ""<WaveXRController>/rightXHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonXPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b0cd5cf-9ed7-4f0f-9786-8c4f2dc3567d"",
                    ""path"": ""<WaveXRController>/rightYHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonYPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82785567-8a0b-4023-86f6-51bf30e585c2"",
                    ""path"": ""<WaveXRController>/rightBackHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""BackPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af4af28f-b4a6-4510-9368-9405a289c229"",
                    ""path"": ""<WaveXRController>/rightEnterHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""EnterPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7261256a-c823-445f-8981-ecf6246f3e31"",
                    ""path"": ""<WaveXRController>/rightTouchpadHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TouchpadPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8dc36828-f6d3-48c6-812e-fefd82663db3"",
                    ""path"": ""<WaveXRController>/rightBumperHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60fb5c7d-695e-4d32-b552-e347f67eebb4"",
                    ""path"": ""<WaveXRController>/rightTriggerHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5aeed9a2-9561-4c15-b430-afca41daa49f"",
                    ""path"": ""<WaveXRController>/rightJoystickHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""JoystickPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""126d4037-1988-4e44-92bb-4d1bb34e8b9c"",
                    ""path"": ""<WaveXRController>/rightJoystickTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""JoystickTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8bc49a79-73e4-4836-a770-a8106c8c573e"",
                    ""path"": ""<WaveXRController>/rightParkingTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ParkingTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f6ff6bb-c5e0-466d-8c23-16427a141ed7"",
                    ""path"": ""<WaveXRController>/rightGripTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""GripTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8a25f1c-9c0f-4a83-a138-916d754f1f02"",
                    ""path"": ""<WaveXRController>/rightATouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonATouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6caf6840-2603-48ed-b0aa-0da63fe457c5"",
                    ""path"": ""<WaveXRController>/rightBTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonBTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""176f7c12-be53-419b-8ff7-915a181231e0"",
                    ""path"": ""<WaveXRController>/rightXTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonXTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9cd1671e-110a-404a-a1a7-e10d23faac42"",
                    ""path"": ""<WaveXRController>/rightYTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""ButtonYTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8c222b4-ce65-405d-beab-5fd3486a7d2d"",
                    ""path"": ""<WaveXRController>/rightTouchpadTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TouchpadTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f233c00-a895-4475-876f-692dfc5ed8be"",
                    ""path"": ""<WaveXRController>/rightBumperTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03846cb9-eb2a-492a-b9c6-02b5cc76f639"",
                    ""path"": ""<WaveXRController>/rightTriggerTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3581154-23d0-49a9-be07-4ae24b2a5e2a"",
                    ""path"": ""<WaveXRController>/rightTouchpadAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TouchpadAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45b12e92-2de7-49bb-9eea-2fa4ad77be69"",
                    ""path"": ""<WaveXRController>/rightBumerAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d53f2b2e-daef-4edf-ae33-dd4eff0a8456"",
                    ""path"": ""<WaveXRController>/rightTriggerAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""TriggerAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73cee063-0daf-4719-a548-405e37a2e15a"",
                    ""path"": ""<WaveXRController>/rightJoystickAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""JoystickAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe83b6c2-bc93-4e9b-a6f0-bd862fdf0e77"",
                    ""path"": ""<WaveXRController>/rightGripAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""GripAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Hmd"",
            ""id"": ""e032df07-7def-4193-8b53-7ece4e4a48bc"",
            ""actions"": [
                {
                    ""name"": ""SystemPress"",
                    ""type"": ""Button"",
                    ""id"": ""4f91a784-53bc-46a0-be3c-554d8f57c0dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BackPress"",
                    ""type"": ""Button"",
                    ""id"": ""936c5ddf-44f5-46df-a690-ff4e2664d679"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EnterPress"",
                    ""type"": ""Button"",
                    ""id"": ""5fc098a9-61dd-4bb2-91ad-9711eadd5867"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f29c81ad-c54a-4b70-89eb-b13806b0cd0d"",
                    ""path"": ""<WaveXRHmd>/hmdSystemHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""SystemPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6445341a-c03e-41fc-aeaa-09f080da3332"",
                    ""path"": ""<WaveXRHmd>/hmdBackHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""BackPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8477247-d387-4b6c-ab8e-1f01469ecd92"",
                    ""path"": ""<WaveXRHmd>/hmdEnterHold"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WaveXR"",
                    ""action"": ""EnterPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""WaveXR"",
            ""bindingGroup"": ""WaveXR"",
            ""devices"": []
        }
    ]
}");
            // Left
            m_Left = asset.FindActionMap("Left", throwIfNotFound: true);
            m_Left_SystemPress = m_Left.FindAction("SystemPress", throwIfNotFound: true);
            m_Left_MenuPress = m_Left.FindAction("MenuPress", throwIfNotFound: true);
            m_Left_GripPress = m_Left.FindAction("GripPress", throwIfNotFound: true);
            m_Left_GripTouch = m_Left.FindAction("GripTouch", throwIfNotFound: true);
            m_Left_GripAxis = m_Left.FindAction("GripAxis", throwIfNotFound: true);
            m_Left_VolumeUpPress = m_Left.FindAction("VolumeUpPress", throwIfNotFound: true);
            m_Left_VolumeDownPress = m_Left.FindAction("VolumeDownPress", throwIfNotFound: true);
            m_Left_ButtonAPress = m_Left.FindAction("ButtonAPress", throwIfNotFound: true);
            m_Left_ButtonATouch = m_Left.FindAction("ButtonATouch", throwIfNotFound: true);
            m_Left_ButtonBPress = m_Left.FindAction("ButtonBPress", throwIfNotFound: true);
            m_Left_ButtonBTouch = m_Left.FindAction("ButtonBTouch", throwIfNotFound: true);
            m_Left_ButtonXPress = m_Left.FindAction("ButtonXPress", throwIfNotFound: true);
            m_Left_ButtonXTouch = m_Left.FindAction("ButtonXTouch", throwIfNotFound: true);
            m_Left_ButtonYPress = m_Left.FindAction("ButtonYPress", throwIfNotFound: true);
            m_Left_ButtonYTouch = m_Left.FindAction("ButtonYTouch", throwIfNotFound: true);
            m_Left_BackPress = m_Left.FindAction("BackPress", throwIfNotFound: true);
            m_Left_EnterPress = m_Left.FindAction("EnterPress", throwIfNotFound: true);
            m_Left_TouchpadPress = m_Left.FindAction("TouchpadPress", throwIfNotFound: true);
            m_Left_TouchpadTouch = m_Left.FindAction("TouchpadTouch", throwIfNotFound: true);
            m_Left_TouchpadAxis = m_Left.FindAction("TouchpadAxis", throwIfNotFound: true);
            m_Left_TriggerPress = m_Left.FindAction("TriggerPress", throwIfNotFound: true);
            m_Left_TriggerTouch = m_Left.FindAction("TriggerTouch", throwIfNotFound: true);
            m_Left_TriggerAxis = m_Left.FindAction("TriggerAxis", throwIfNotFound: true);
            m_Left_JoystickPress = m_Left.FindAction("JoystickPress", throwIfNotFound: true);
            m_Left_JoystickTouch = m_Left.FindAction("JoystickTouch", throwIfNotFound: true);
            m_Left_JoystickAxis = m_Left.FindAction("JoystickAxis", throwIfNotFound: true);
            m_Left_ParkingTouch = m_Left.FindAction("ParkingTouch", throwIfNotFound: true);
            // Right
            m_Right = asset.FindActionMap("Right", throwIfNotFound: true);
            m_Right_SystemPress = m_Right.FindAction("SystemPress", throwIfNotFound: true);
            m_Right_MenuPress = m_Right.FindAction("MenuPress", throwIfNotFound: true);
            m_Right_GripPress = m_Right.FindAction("GripPress", throwIfNotFound: true);
            m_Right_GripTouch = m_Right.FindAction("GripTouch", throwIfNotFound: true);
            m_Right_GripAxis = m_Right.FindAction("GripAxis", throwIfNotFound: true);
            m_Right_VolumeUpPress = m_Right.FindAction("VolumeUpPress", throwIfNotFound: true);
            m_Right_VolumeDownPress = m_Right.FindAction("VolumeDownPress", throwIfNotFound: true);
            m_Right_ButtonAPress = m_Right.FindAction("ButtonAPress", throwIfNotFound: true);
            m_Right_ButtonATouch = m_Right.FindAction("ButtonATouch", throwIfNotFound: true);
            m_Right_ButtonBPress = m_Right.FindAction("ButtonBPress", throwIfNotFound: true);
            m_Right_ButtonBTouch = m_Right.FindAction("ButtonBTouch", throwIfNotFound: true);
            m_Right_ButtonXPress = m_Right.FindAction("ButtonXPress", throwIfNotFound: true);
            m_Right_ButtonXTouch = m_Right.FindAction("ButtonXTouch", throwIfNotFound: true);
            m_Right_ButtonYPress = m_Right.FindAction("ButtonYPress", throwIfNotFound: true);
            m_Right_ButtonYTouch = m_Right.FindAction("ButtonYTouch", throwIfNotFound: true);
            m_Right_BackPress = m_Right.FindAction("BackPress", throwIfNotFound: true);
            m_Right_EnterPress = m_Right.FindAction("EnterPress", throwIfNotFound: true);
            m_Right_TouchpadPress = m_Right.FindAction("TouchpadPress", throwIfNotFound: true);
            m_Right_TouchpadTouch = m_Right.FindAction("TouchpadTouch", throwIfNotFound: true);
            m_Right_TouchpadAxis = m_Right.FindAction("TouchpadAxis", throwIfNotFound: true);
            m_Right_TriggerPress = m_Right.FindAction("TriggerPress", throwIfNotFound: true);
            m_Right_TriggerTouch = m_Right.FindAction("TriggerTouch", throwIfNotFound: true);
            m_Right_TriggerAxis = m_Right.FindAction("TriggerAxis", throwIfNotFound: true);
            m_Right_JoystickPress = m_Right.FindAction("JoystickPress", throwIfNotFound: true);
            m_Right_JoystickTouch = m_Right.FindAction("JoystickTouch", throwIfNotFound: true);
            m_Right_JoystickAxis = m_Right.FindAction("JoystickAxis", throwIfNotFound: true);
            m_Right_ParkingTouch = m_Right.FindAction("ParkingTouch", throwIfNotFound: true);
            // Hmd
            m_Hmd = asset.FindActionMap("Hmd", throwIfNotFound: true);
            m_Hmd_SystemPress = m_Hmd.FindAction("SystemPress", throwIfNotFound: true);
            m_Hmd_BackPress = m_Hmd.FindAction("BackPress", throwIfNotFound: true);
            m_Hmd_EnterPress = m_Hmd.FindAction("EnterPress", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Left
        private readonly InputActionMap m_Left;
        private ILeftActions m_LeftActionsCallbackInterface;
        private readonly InputAction m_Left_SystemPress;
        private readonly InputAction m_Left_MenuPress;
        private readonly InputAction m_Left_GripPress;
        private readonly InputAction m_Left_GripTouch;
        private readonly InputAction m_Left_GripAxis;
        private readonly InputAction m_Left_VolumeUpPress;
        private readonly InputAction m_Left_VolumeDownPress;
        private readonly InputAction m_Left_ButtonAPress;
        private readonly InputAction m_Left_ButtonATouch;
        private readonly InputAction m_Left_ButtonBPress;
        private readonly InputAction m_Left_ButtonBTouch;
        private readonly InputAction m_Left_ButtonXPress;
        private readonly InputAction m_Left_ButtonXTouch;
        private readonly InputAction m_Left_ButtonYPress;
        private readonly InputAction m_Left_ButtonYTouch;
        private readonly InputAction m_Left_BackPress;
        private readonly InputAction m_Left_EnterPress;
        private readonly InputAction m_Left_TouchpadPress;
        private readonly InputAction m_Left_TouchpadTouch;
        private readonly InputAction m_Left_TouchpadAxis;
        private readonly InputAction m_Left_TriggerPress;
        private readonly InputAction m_Left_TriggerTouch;
        private readonly InputAction m_Left_TriggerAxis;
        private readonly InputAction m_Left_JoystickPress;
        private readonly InputAction m_Left_JoystickTouch;
        private readonly InputAction m_Left_JoystickAxis;
        private readonly InputAction m_Left_ParkingTouch;
        public struct LeftActions
        {
            private @WaveXRInput m_Wrapper;
            public LeftActions(@WaveXRInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @SystemPress => m_Wrapper.m_Left_SystemPress;
            public InputAction @MenuPress => m_Wrapper.m_Left_MenuPress;
            public InputAction @GripPress => m_Wrapper.m_Left_GripPress;
            public InputAction @GripTouch => m_Wrapper.m_Left_GripTouch;
            public InputAction @GripAxis => m_Wrapper.m_Left_GripAxis;
            public InputAction @VolumeUpPress => m_Wrapper.m_Left_VolumeUpPress;
            public InputAction @VolumeDownPress => m_Wrapper.m_Left_VolumeDownPress;
            public InputAction @ButtonAPress => m_Wrapper.m_Left_ButtonAPress;
            public InputAction @ButtonATouch => m_Wrapper.m_Left_ButtonATouch;
            public InputAction @ButtonBPress => m_Wrapper.m_Left_ButtonBPress;
            public InputAction @ButtonBTouch => m_Wrapper.m_Left_ButtonBTouch;
            public InputAction @ButtonXPress => m_Wrapper.m_Left_ButtonXPress;
            public InputAction @ButtonXTouch => m_Wrapper.m_Left_ButtonXTouch;
            public InputAction @ButtonYPress => m_Wrapper.m_Left_ButtonYPress;
            public InputAction @ButtonYTouch => m_Wrapper.m_Left_ButtonYTouch;
            public InputAction @BackPress => m_Wrapper.m_Left_BackPress;
            public InputAction @EnterPress => m_Wrapper.m_Left_EnterPress;
            public InputAction @TouchpadPress => m_Wrapper.m_Left_TouchpadPress;
            public InputAction @TouchpadTouch => m_Wrapper.m_Left_TouchpadTouch;
            public InputAction @TouchpadAxis => m_Wrapper.m_Left_TouchpadAxis;
            public InputAction @TriggerPress => m_Wrapper.m_Left_TriggerPress;
            public InputAction @TriggerTouch => m_Wrapper.m_Left_TriggerTouch;
            public InputAction @TriggerAxis => m_Wrapper.m_Left_TriggerAxis;
            public InputAction @JoystickPress => m_Wrapper.m_Left_JoystickPress;
            public InputAction @JoystickTouch => m_Wrapper.m_Left_JoystickTouch;
            public InputAction @JoystickAxis => m_Wrapper.m_Left_JoystickAxis;
            public InputAction @ParkingTouch => m_Wrapper.m_Left_ParkingTouch;
            public InputActionMap Get() { return m_Wrapper.m_Left; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(LeftActions set) { return set.Get(); }
            public void SetCallbacks(ILeftActions instance)
            {
                if (m_Wrapper.m_LeftActionsCallbackInterface != null)
                {
                    @SystemPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnSystemPress;
                    @SystemPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnSystemPress;
                    @SystemPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnSystemPress;
                    @MenuPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnMenuPress;
                    @MenuPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnMenuPress;
                    @MenuPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnMenuPress;
                    @GripPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnGripPress;
                    @GripPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnGripPress;
                    @GripPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnGripPress;
                    @GripTouch.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnGripTouch;
                    @GripTouch.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnGripTouch;
                    @GripTouch.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnGripTouch;
                    @GripAxis.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnGripAxis;
                    @GripAxis.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnGripAxis;
                    @GripAxis.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnGripAxis;
                    @VolumeUpPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnVolumeUpPress;
                    @VolumeUpPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnVolumeUpPress;
                    @VolumeUpPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnVolumeUpPress;
                    @VolumeDownPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnVolumeDownPress;
                    @VolumeDownPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnVolumeDownPress;
                    @VolumeDownPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnVolumeDownPress;
                    @ButtonAPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonAPress;
                    @ButtonAPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonAPress;
                    @ButtonAPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonAPress;
                    @ButtonATouch.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonATouch;
                    @ButtonATouch.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonATouch;
                    @ButtonATouch.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonATouch;
                    @ButtonBPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonBPress;
                    @ButtonBPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonBPress;
                    @ButtonBPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonBPress;
                    @ButtonBTouch.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonBTouch;
                    @ButtonBTouch.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonBTouch;
                    @ButtonBTouch.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonBTouch;
                    @ButtonXPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonXPress;
                    @ButtonXPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonXPress;
                    @ButtonXPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonXPress;
                    @ButtonXTouch.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonXTouch;
                    @ButtonXTouch.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonXTouch;
                    @ButtonXTouch.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonXTouch;
                    @ButtonYPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonYPress;
                    @ButtonYPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonYPress;
                    @ButtonYPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonYPress;
                    @ButtonYTouch.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonYTouch;
                    @ButtonYTouch.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonYTouch;
                    @ButtonYTouch.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnButtonYTouch;
                    @BackPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnBackPress;
                    @BackPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnBackPress;
                    @BackPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnBackPress;
                    @EnterPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnEnterPress;
                    @EnterPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnEnterPress;
                    @EnterPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnEnterPress;
                    @TouchpadPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnTouchpadPress;
                    @TouchpadPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnTouchpadPress;
                    @TouchpadPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnTouchpadPress;
                    @TouchpadTouch.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnTouchpadTouch;
                    @TouchpadTouch.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnTouchpadTouch;
                    @TouchpadTouch.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnTouchpadTouch;
                    @TouchpadAxis.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnTouchpadAxis;
                    @TouchpadAxis.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnTouchpadAxis;
                    @TouchpadAxis.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnTouchpadAxis;
                    @TriggerPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerPress;
                    @TriggerPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerPress;
                    @TriggerPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerPress;
                    @TriggerTouch.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerTouch;
                    @TriggerTouch.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerTouch;
                    @TriggerTouch.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerTouch;
                    @TriggerAxis.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerAxis;
                    @TriggerAxis.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerAxis;
                    @TriggerAxis.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnTriggerAxis;
                    @JoystickPress.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystickPress;
                    @JoystickPress.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystickPress;
                    @JoystickPress.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystickPress;
                    @JoystickTouch.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystickTouch;
                    @JoystickTouch.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystickTouch;
                    @JoystickTouch.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystickTouch;
                    @JoystickAxis.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystickAxis;
                    @JoystickAxis.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystickAxis;
                    @JoystickAxis.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnJoystickAxis;
                    @ParkingTouch.started -= m_Wrapper.m_LeftActionsCallbackInterface.OnParkingTouch;
                    @ParkingTouch.performed -= m_Wrapper.m_LeftActionsCallbackInterface.OnParkingTouch;
                    @ParkingTouch.canceled -= m_Wrapper.m_LeftActionsCallbackInterface.OnParkingTouch;
                }
                m_Wrapper.m_LeftActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @SystemPress.started += instance.OnSystemPress;
                    @SystemPress.performed += instance.OnSystemPress;
                    @SystemPress.canceled += instance.OnSystemPress;
                    @MenuPress.started += instance.OnMenuPress;
                    @MenuPress.performed += instance.OnMenuPress;
                    @MenuPress.canceled += instance.OnMenuPress;
                    @GripPress.started += instance.OnGripPress;
                    @GripPress.performed += instance.OnGripPress;
                    @GripPress.canceled += instance.OnGripPress;
                    @GripTouch.started += instance.OnGripTouch;
                    @GripTouch.performed += instance.OnGripTouch;
                    @GripTouch.canceled += instance.OnGripTouch;
                    @GripAxis.started += instance.OnGripAxis;
                    @GripAxis.performed += instance.OnGripAxis;
                    @GripAxis.canceled += instance.OnGripAxis;
                    @VolumeUpPress.started += instance.OnVolumeUpPress;
                    @VolumeUpPress.performed += instance.OnVolumeUpPress;
                    @VolumeUpPress.canceled += instance.OnVolumeUpPress;
                    @VolumeDownPress.started += instance.OnVolumeDownPress;
                    @VolumeDownPress.performed += instance.OnVolumeDownPress;
                    @VolumeDownPress.canceled += instance.OnVolumeDownPress;
                    @ButtonAPress.started += instance.OnButtonAPress;
                    @ButtonAPress.performed += instance.OnButtonAPress;
                    @ButtonAPress.canceled += instance.OnButtonAPress;
                    @ButtonATouch.started += instance.OnButtonATouch;
                    @ButtonATouch.performed += instance.OnButtonATouch;
                    @ButtonATouch.canceled += instance.OnButtonATouch;
                    @ButtonBPress.started += instance.OnButtonBPress;
                    @ButtonBPress.performed += instance.OnButtonBPress;
                    @ButtonBPress.canceled += instance.OnButtonBPress;
                    @ButtonBTouch.started += instance.OnButtonBTouch;
                    @ButtonBTouch.performed += instance.OnButtonBTouch;
                    @ButtonBTouch.canceled += instance.OnButtonBTouch;
                    @ButtonXPress.started += instance.OnButtonXPress;
                    @ButtonXPress.performed += instance.OnButtonXPress;
                    @ButtonXPress.canceled += instance.OnButtonXPress;
                    @ButtonXTouch.started += instance.OnButtonXTouch;
                    @ButtonXTouch.performed += instance.OnButtonXTouch;
                    @ButtonXTouch.canceled += instance.OnButtonXTouch;
                    @ButtonYPress.started += instance.OnButtonYPress;
                    @ButtonYPress.performed += instance.OnButtonYPress;
                    @ButtonYPress.canceled += instance.OnButtonYPress;
                    @ButtonYTouch.started += instance.OnButtonYTouch;
                    @ButtonYTouch.performed += instance.OnButtonYTouch;
                    @ButtonYTouch.canceled += instance.OnButtonYTouch;
                    @BackPress.started += instance.OnBackPress;
                    @BackPress.performed += instance.OnBackPress;
                    @BackPress.canceled += instance.OnBackPress;
                    @EnterPress.started += instance.OnEnterPress;
                    @EnterPress.performed += instance.OnEnterPress;
                    @EnterPress.canceled += instance.OnEnterPress;
                    @TouchpadPress.started += instance.OnTouchpadPress;
                    @TouchpadPress.performed += instance.OnTouchpadPress;
                    @TouchpadPress.canceled += instance.OnTouchpadPress;
                    @TouchpadTouch.started += instance.OnTouchpadTouch;
                    @TouchpadTouch.performed += instance.OnTouchpadTouch;
                    @TouchpadTouch.canceled += instance.OnTouchpadTouch;
                    @TouchpadAxis.started += instance.OnTouchpadAxis;
                    @TouchpadAxis.performed += instance.OnTouchpadAxis;
                    @TouchpadAxis.canceled += instance.OnTouchpadAxis;
                    @TriggerPress.started += instance.OnTriggerPress;
                    @TriggerPress.performed += instance.OnTriggerPress;
                    @TriggerPress.canceled += instance.OnTriggerPress;
                    @TriggerTouch.started += instance.OnTriggerTouch;
                    @TriggerTouch.performed += instance.OnTriggerTouch;
                    @TriggerTouch.canceled += instance.OnTriggerTouch;
                    @TriggerAxis.started += instance.OnTriggerAxis;
                    @TriggerAxis.performed += instance.OnTriggerAxis;
                    @TriggerAxis.canceled += instance.OnTriggerAxis;
                    @JoystickPress.started += instance.OnJoystickPress;
                    @JoystickPress.performed += instance.OnJoystickPress;
                    @JoystickPress.canceled += instance.OnJoystickPress;
                    @JoystickTouch.started += instance.OnJoystickTouch;
                    @JoystickTouch.performed += instance.OnJoystickTouch;
                    @JoystickTouch.canceled += instance.OnJoystickTouch;
                    @JoystickAxis.started += instance.OnJoystickAxis;
                    @JoystickAxis.performed += instance.OnJoystickAxis;
                    @JoystickAxis.canceled += instance.OnJoystickAxis;
                    @ParkingTouch.started += instance.OnParkingTouch;
                    @ParkingTouch.performed += instance.OnParkingTouch;
                    @ParkingTouch.canceled += instance.OnParkingTouch;
                }
            }
        }
        public LeftActions @Left => new LeftActions(this);

        // Right
        private readonly InputActionMap m_Right;
        private IRightActions m_RightActionsCallbackInterface;
        private readonly InputAction m_Right_SystemPress;
        private readonly InputAction m_Right_MenuPress;
        private readonly InputAction m_Right_GripPress;
        private readonly InputAction m_Right_GripTouch;
        private readonly InputAction m_Right_GripAxis;
        private readonly InputAction m_Right_VolumeUpPress;
        private readonly InputAction m_Right_VolumeDownPress;
        private readonly InputAction m_Right_ButtonAPress;
        private readonly InputAction m_Right_ButtonATouch;
        private readonly InputAction m_Right_ButtonBPress;
        private readonly InputAction m_Right_ButtonBTouch;
        private readonly InputAction m_Right_ButtonXPress;
        private readonly InputAction m_Right_ButtonXTouch;
        private readonly InputAction m_Right_ButtonYPress;
        private readonly InputAction m_Right_ButtonYTouch;
        private readonly InputAction m_Right_BackPress;
        private readonly InputAction m_Right_EnterPress;
        private readonly InputAction m_Right_TouchpadPress;
        private readonly InputAction m_Right_TouchpadTouch;
        private readonly InputAction m_Right_TouchpadAxis;
        private readonly InputAction m_Right_TriggerPress;
        private readonly InputAction m_Right_TriggerTouch;
        private readonly InputAction m_Right_TriggerAxis;
        private readonly InputAction m_Right_JoystickPress;
        private readonly InputAction m_Right_JoystickTouch;
        private readonly InputAction m_Right_JoystickAxis;
        private readonly InputAction m_Right_ParkingTouch;
        public struct RightActions
        {
            private @WaveXRInput m_Wrapper;
            public RightActions(@WaveXRInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @SystemPress => m_Wrapper.m_Right_SystemPress;
            public InputAction @MenuPress => m_Wrapper.m_Right_MenuPress;
            public InputAction @GripPress => m_Wrapper.m_Right_GripPress;
            public InputAction @GripTouch => m_Wrapper.m_Right_GripTouch;
            public InputAction @GripAxis => m_Wrapper.m_Right_GripAxis;
            public InputAction @VolumeUpPress => m_Wrapper.m_Right_VolumeUpPress;
            public InputAction @VolumeDownPress => m_Wrapper.m_Right_VolumeDownPress;
            public InputAction @ButtonAPress => m_Wrapper.m_Right_ButtonAPress;
            public InputAction @ButtonATouch => m_Wrapper.m_Right_ButtonATouch;
            public InputAction @ButtonBPress => m_Wrapper.m_Right_ButtonBPress;
            public InputAction @ButtonBTouch => m_Wrapper.m_Right_ButtonBTouch;
            public InputAction @ButtonXPress => m_Wrapper.m_Right_ButtonXPress;
            public InputAction @ButtonXTouch => m_Wrapper.m_Right_ButtonXTouch;
            public InputAction @ButtonYPress => m_Wrapper.m_Right_ButtonYPress;
            public InputAction @ButtonYTouch => m_Wrapper.m_Right_ButtonYTouch;
            public InputAction @BackPress => m_Wrapper.m_Right_BackPress;
            public InputAction @EnterPress => m_Wrapper.m_Right_EnterPress;
            public InputAction @TouchpadPress => m_Wrapper.m_Right_TouchpadPress;
            public InputAction @TouchpadTouch => m_Wrapper.m_Right_TouchpadTouch;
            public InputAction @TouchpadAxis => m_Wrapper.m_Right_TouchpadAxis;
            public InputAction @TriggerPress => m_Wrapper.m_Right_TriggerPress;
            public InputAction @TriggerTouch => m_Wrapper.m_Right_TriggerTouch;
            public InputAction @TriggerAxis => m_Wrapper.m_Right_TriggerAxis;
            public InputAction @JoystickPress => m_Wrapper.m_Right_JoystickPress;
            public InputAction @JoystickTouch => m_Wrapper.m_Right_JoystickTouch;
            public InputAction @JoystickAxis => m_Wrapper.m_Right_JoystickAxis;
            public InputAction @ParkingTouch => m_Wrapper.m_Right_ParkingTouch;
            public InputActionMap Get() { return m_Wrapper.m_Right; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(RightActions set) { return set.Get(); }
            public void SetCallbacks(IRightActions instance)
            {
                if (m_Wrapper.m_RightActionsCallbackInterface != null)
                {
                    @SystemPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnSystemPress;
                    @SystemPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnSystemPress;
                    @SystemPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnSystemPress;
                    @MenuPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnMenuPress;
                    @MenuPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnMenuPress;
                    @MenuPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnMenuPress;
                    @GripPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnGripPress;
                    @GripPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnGripPress;
                    @GripPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnGripPress;
                    @GripTouch.started -= m_Wrapper.m_RightActionsCallbackInterface.OnGripTouch;
                    @GripTouch.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnGripTouch;
                    @GripTouch.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnGripTouch;
                    @GripAxis.started -= m_Wrapper.m_RightActionsCallbackInterface.OnGripAxis;
                    @GripAxis.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnGripAxis;
                    @GripAxis.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnGripAxis;
                    @VolumeUpPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnVolumeUpPress;
                    @VolumeUpPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnVolumeUpPress;
                    @VolumeUpPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnVolumeUpPress;
                    @VolumeDownPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnVolumeDownPress;
                    @VolumeDownPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnVolumeDownPress;
                    @VolumeDownPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnVolumeDownPress;
                    @ButtonAPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonAPress;
                    @ButtonAPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonAPress;
                    @ButtonAPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonAPress;
                    @ButtonATouch.started -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonATouch;
                    @ButtonATouch.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonATouch;
                    @ButtonATouch.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonATouch;
                    @ButtonBPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonBPress;
                    @ButtonBPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonBPress;
                    @ButtonBPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonBPress;
                    @ButtonBTouch.started -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonBTouch;
                    @ButtonBTouch.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonBTouch;
                    @ButtonBTouch.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonBTouch;
                    @ButtonXPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonXPress;
                    @ButtonXPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonXPress;
                    @ButtonXPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonXPress;
                    @ButtonXTouch.started -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonXTouch;
                    @ButtonXTouch.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonXTouch;
                    @ButtonXTouch.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonXTouch;
                    @ButtonYPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonYPress;
                    @ButtonYPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonYPress;
                    @ButtonYPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonYPress;
                    @ButtonYTouch.started -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonYTouch;
                    @ButtonYTouch.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonYTouch;
                    @ButtonYTouch.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnButtonYTouch;
                    @BackPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnBackPress;
                    @BackPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnBackPress;
                    @BackPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnBackPress;
                    @EnterPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnEnterPress;
                    @EnterPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnEnterPress;
                    @EnterPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnEnterPress;
                    @TouchpadPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnTouchpadPress;
                    @TouchpadPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnTouchpadPress;
                    @TouchpadPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnTouchpadPress;
                    @TouchpadTouch.started -= m_Wrapper.m_RightActionsCallbackInterface.OnTouchpadTouch;
                    @TouchpadTouch.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnTouchpadTouch;
                    @TouchpadTouch.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnTouchpadTouch;
                    @TouchpadAxis.started -= m_Wrapper.m_RightActionsCallbackInterface.OnTouchpadAxis;
                    @TouchpadAxis.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnTouchpadAxis;
                    @TouchpadAxis.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnTouchpadAxis;
                    @TriggerPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerPress;
                    @TriggerPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerPress;
                    @TriggerPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerPress;
                    @TriggerTouch.started -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerTouch;
                    @TriggerTouch.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerTouch;
                    @TriggerTouch.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerTouch;
                    @TriggerAxis.started -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerAxis;
                    @TriggerAxis.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerAxis;
                    @TriggerAxis.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnTriggerAxis;
                    @JoystickPress.started -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystickPress;
                    @JoystickPress.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystickPress;
                    @JoystickPress.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystickPress;
                    @JoystickTouch.started -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystickTouch;
                    @JoystickTouch.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystickTouch;
                    @JoystickTouch.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystickTouch;
                    @JoystickAxis.started -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystickAxis;
                    @JoystickAxis.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystickAxis;
                    @JoystickAxis.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnJoystickAxis;
                    @ParkingTouch.started -= m_Wrapper.m_RightActionsCallbackInterface.OnParkingTouch;
                    @ParkingTouch.performed -= m_Wrapper.m_RightActionsCallbackInterface.OnParkingTouch;
                    @ParkingTouch.canceled -= m_Wrapper.m_RightActionsCallbackInterface.OnParkingTouch;
                }
                m_Wrapper.m_RightActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @SystemPress.started += instance.OnSystemPress;
                    @SystemPress.performed += instance.OnSystemPress;
                    @SystemPress.canceled += instance.OnSystemPress;
                    @MenuPress.started += instance.OnMenuPress;
                    @MenuPress.performed += instance.OnMenuPress;
                    @MenuPress.canceled += instance.OnMenuPress;
                    @GripPress.started += instance.OnGripPress;
                    @GripPress.performed += instance.OnGripPress;
                    @GripPress.canceled += instance.OnGripPress;
                    @GripTouch.started += instance.OnGripTouch;
                    @GripTouch.performed += instance.OnGripTouch;
                    @GripTouch.canceled += instance.OnGripTouch;
                    @GripAxis.started += instance.OnGripAxis;
                    @GripAxis.performed += instance.OnGripAxis;
                    @GripAxis.canceled += instance.OnGripAxis;
                    @VolumeUpPress.started += instance.OnVolumeUpPress;
                    @VolumeUpPress.performed += instance.OnVolumeUpPress;
                    @VolumeUpPress.canceled += instance.OnVolumeUpPress;
                    @VolumeDownPress.started += instance.OnVolumeDownPress;
                    @VolumeDownPress.performed += instance.OnVolumeDownPress;
                    @VolumeDownPress.canceled += instance.OnVolumeDownPress;
                    @ButtonAPress.started += instance.OnButtonAPress;
                    @ButtonAPress.performed += instance.OnButtonAPress;
                    @ButtonAPress.canceled += instance.OnButtonAPress;
                    @ButtonATouch.started += instance.OnButtonATouch;
                    @ButtonATouch.performed += instance.OnButtonATouch;
                    @ButtonATouch.canceled += instance.OnButtonATouch;
                    @ButtonBPress.started += instance.OnButtonBPress;
                    @ButtonBPress.performed += instance.OnButtonBPress;
                    @ButtonBPress.canceled += instance.OnButtonBPress;
                    @ButtonBTouch.started += instance.OnButtonBTouch;
                    @ButtonBTouch.performed += instance.OnButtonBTouch;
                    @ButtonBTouch.canceled += instance.OnButtonBTouch;
                    @ButtonXPress.started += instance.OnButtonXPress;
                    @ButtonXPress.performed += instance.OnButtonXPress;
                    @ButtonXPress.canceled += instance.OnButtonXPress;
                    @ButtonXTouch.started += instance.OnButtonXTouch;
                    @ButtonXTouch.performed += instance.OnButtonXTouch;
                    @ButtonXTouch.canceled += instance.OnButtonXTouch;
                    @ButtonYPress.started += instance.OnButtonYPress;
                    @ButtonYPress.performed += instance.OnButtonYPress;
                    @ButtonYPress.canceled += instance.OnButtonYPress;
                    @ButtonYTouch.started += instance.OnButtonYTouch;
                    @ButtonYTouch.performed += instance.OnButtonYTouch;
                    @ButtonYTouch.canceled += instance.OnButtonYTouch;
                    @BackPress.started += instance.OnBackPress;
                    @BackPress.performed += instance.OnBackPress;
                    @BackPress.canceled += instance.OnBackPress;
                    @EnterPress.started += instance.OnEnterPress;
                    @EnterPress.performed += instance.OnEnterPress;
                    @EnterPress.canceled += instance.OnEnterPress;
                    @TouchpadPress.started += instance.OnTouchpadPress;
                    @TouchpadPress.performed += instance.OnTouchpadPress;
                    @TouchpadPress.canceled += instance.OnTouchpadPress;
                    @TouchpadTouch.started += instance.OnTouchpadTouch;
                    @TouchpadTouch.performed += instance.OnTouchpadTouch;
                    @TouchpadTouch.canceled += instance.OnTouchpadTouch;
                    @TouchpadAxis.started += instance.OnTouchpadAxis;
                    @TouchpadAxis.performed += instance.OnTouchpadAxis;
                    @TouchpadAxis.canceled += instance.OnTouchpadAxis;
                    @TriggerPress.started += instance.OnTriggerPress;
                    @TriggerPress.performed += instance.OnTriggerPress;
                    @TriggerPress.canceled += instance.OnTriggerPress;
                    @TriggerTouch.started += instance.OnTriggerTouch;
                    @TriggerTouch.performed += instance.OnTriggerTouch;
                    @TriggerTouch.canceled += instance.OnTriggerTouch;
                    @TriggerAxis.started += instance.OnTriggerAxis;
                    @TriggerAxis.performed += instance.OnTriggerAxis;
                    @TriggerAxis.canceled += instance.OnTriggerAxis;
                    @JoystickPress.started += instance.OnJoystickPress;
                    @JoystickPress.performed += instance.OnJoystickPress;
                    @JoystickPress.canceled += instance.OnJoystickPress;
                    @JoystickTouch.started += instance.OnJoystickTouch;
                    @JoystickTouch.performed += instance.OnJoystickTouch;
                    @JoystickTouch.canceled += instance.OnJoystickTouch;
                    @JoystickAxis.started += instance.OnJoystickAxis;
                    @JoystickAxis.performed += instance.OnJoystickAxis;
                    @JoystickAxis.canceled += instance.OnJoystickAxis;
                    @ParkingTouch.started += instance.OnParkingTouch;
                    @ParkingTouch.performed += instance.OnParkingTouch;
                    @ParkingTouch.canceled += instance.OnParkingTouch;
                }
            }
        }
        public RightActions @Right => new RightActions(this);

        // Hmd
        private readonly InputActionMap m_Hmd;
        private IHmdActions m_HmdActionsCallbackInterface;
        private readonly InputAction m_Hmd_SystemPress;
        private readonly InputAction m_Hmd_BackPress;
        private readonly InputAction m_Hmd_EnterPress;
        public struct HmdActions
        {
            private @WaveXRInput m_Wrapper;
            public HmdActions(@WaveXRInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @SystemPress => m_Wrapper.m_Hmd_SystemPress;
            public InputAction @BackPress => m_Wrapper.m_Hmd_BackPress;
            public InputAction @EnterPress => m_Wrapper.m_Hmd_EnterPress;
            public InputActionMap Get() { return m_Wrapper.m_Hmd; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(HmdActions set) { return set.Get(); }
            public void SetCallbacks(IHmdActions instance)
            {
                if (m_Wrapper.m_HmdActionsCallbackInterface != null)
                {
                    @SystemPress.started -= m_Wrapper.m_HmdActionsCallbackInterface.OnSystemPress;
                    @SystemPress.performed -= m_Wrapper.m_HmdActionsCallbackInterface.OnSystemPress;
                    @SystemPress.canceled -= m_Wrapper.m_HmdActionsCallbackInterface.OnSystemPress;
                    @BackPress.started -= m_Wrapper.m_HmdActionsCallbackInterface.OnBackPress;
                    @BackPress.performed -= m_Wrapper.m_HmdActionsCallbackInterface.OnBackPress;
                    @BackPress.canceled -= m_Wrapper.m_HmdActionsCallbackInterface.OnBackPress;
                    @EnterPress.started -= m_Wrapper.m_HmdActionsCallbackInterface.OnEnterPress;
                    @EnterPress.performed -= m_Wrapper.m_HmdActionsCallbackInterface.OnEnterPress;
                    @EnterPress.canceled -= m_Wrapper.m_HmdActionsCallbackInterface.OnEnterPress;
                }
                m_Wrapper.m_HmdActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @SystemPress.started += instance.OnSystemPress;
                    @SystemPress.performed += instance.OnSystemPress;
                    @SystemPress.canceled += instance.OnSystemPress;
                    @BackPress.started += instance.OnBackPress;
                    @BackPress.performed += instance.OnBackPress;
                    @BackPress.canceled += instance.OnBackPress;
                    @EnterPress.started += instance.OnEnterPress;
                    @EnterPress.performed += instance.OnEnterPress;
                    @EnterPress.canceled += instance.OnEnterPress;
                }
            }
        }
        public HmdActions @Hmd => new HmdActions(this);
        private int m_WaveXRSchemeIndex = -1;
        public InputControlScheme WaveXRScheme
        {
            get
            {
                if (m_WaveXRSchemeIndex == -1) m_WaveXRSchemeIndex = asset.FindControlSchemeIndex("WaveXR");
                return asset.controlSchemes[m_WaveXRSchemeIndex];
            }
        }
        public interface ILeftActions
        {
            void OnSystemPress(InputAction.CallbackContext context);
            void OnMenuPress(InputAction.CallbackContext context);
            void OnGripPress(InputAction.CallbackContext context);
            void OnGripTouch(InputAction.CallbackContext context);
            void OnGripAxis(InputAction.CallbackContext context);
            void OnVolumeUpPress(InputAction.CallbackContext context);
            void OnVolumeDownPress(InputAction.CallbackContext context);
            void OnButtonAPress(InputAction.CallbackContext context);
            void OnButtonATouch(InputAction.CallbackContext context);
            void OnButtonBPress(InputAction.CallbackContext context);
            void OnButtonBTouch(InputAction.CallbackContext context);
            void OnButtonXPress(InputAction.CallbackContext context);
            void OnButtonXTouch(InputAction.CallbackContext context);
            void OnButtonYPress(InputAction.CallbackContext context);
            void OnButtonYTouch(InputAction.CallbackContext context);
            void OnBackPress(InputAction.CallbackContext context);
            void OnEnterPress(InputAction.CallbackContext context);
            void OnTouchpadPress(InputAction.CallbackContext context);
            void OnTouchpadTouch(InputAction.CallbackContext context);
            void OnTouchpadAxis(InputAction.CallbackContext context);
            void OnTriggerPress(InputAction.CallbackContext context);
            void OnTriggerTouch(InputAction.CallbackContext context);
            void OnTriggerAxis(InputAction.CallbackContext context);
            void OnJoystickPress(InputAction.CallbackContext context);
            void OnJoystickTouch(InputAction.CallbackContext context);
            void OnJoystickAxis(InputAction.CallbackContext context);
            void OnParkingTouch(InputAction.CallbackContext context);
        }
        public interface IRightActions
        {
            void OnSystemPress(InputAction.CallbackContext context);
            void OnMenuPress(InputAction.CallbackContext context);
            void OnGripPress(InputAction.CallbackContext context);
            void OnGripTouch(InputAction.CallbackContext context);
            void OnGripAxis(InputAction.CallbackContext context);
            void OnVolumeUpPress(InputAction.CallbackContext context);
            void OnVolumeDownPress(InputAction.CallbackContext context);
            void OnButtonAPress(InputAction.CallbackContext context);
            void OnButtonATouch(InputAction.CallbackContext context);
            void OnButtonBPress(InputAction.CallbackContext context);
            void OnButtonBTouch(InputAction.CallbackContext context);
            void OnButtonXPress(InputAction.CallbackContext context);
            void OnButtonXTouch(InputAction.CallbackContext context);
            void OnButtonYPress(InputAction.CallbackContext context);
            void OnButtonYTouch(InputAction.CallbackContext context);
            void OnBackPress(InputAction.CallbackContext context);
            void OnEnterPress(InputAction.CallbackContext context);
            void OnTouchpadPress(InputAction.CallbackContext context);
            void OnTouchpadTouch(InputAction.CallbackContext context);
            void OnTouchpadAxis(InputAction.CallbackContext context);
            void OnTriggerPress(InputAction.CallbackContext context);
            void OnTriggerTouch(InputAction.CallbackContext context);
            void OnTriggerAxis(InputAction.CallbackContext context);
            void OnJoystickPress(InputAction.CallbackContext context);
            void OnJoystickTouch(InputAction.CallbackContext context);
            void OnJoystickAxis(InputAction.CallbackContext context);
            void OnParkingTouch(InputAction.CallbackContext context);
        }
        public interface IHmdActions
        {
            void OnSystemPress(InputAction.CallbackContext context);
            void OnBackPress(InputAction.CallbackContext context);
            void OnEnterPress(InputAction.CallbackContext context);
        }
    }
}
#endif