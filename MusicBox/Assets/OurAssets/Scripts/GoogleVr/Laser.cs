// Copyright 2017 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#if UNITY_ANDROID && !UNITY_EDITOR
#define RUNNING_ON_ANDROID_DEVICE
#endif  // UNITY_ANDROID && !UNITY_EDITOR


using UnityEngine;

#if UNITY_2017_2_OR_NEWER
#else
using XRSettings = UnityEngine.VR.VRSettings;
#endif  // UNITY_2017_2_OR_NEWER
//Manage the teleportation
public class Laser : MonoBehaviour
{
    [Tooltip("Reference to GvrControllerPointer")]
    public GameObject controllerPointer;
    public GameObject player;



    void Start()
    {
        GvrPointerInputModule.Pointer = controllerPointer.GetComponentInChildren<GvrLaserPointer>(true);
    }

    // Runtime switching enabled only in-editor.
    void Update()
    {
        GvrPointerInputModule.Pointer = controllerPointer.GetComponentInChildren<GvrLaserPointer>(true);

        if (GvrControllerInput.AppButtonUp)
        {
            TeleportTo();
        }
    }


    public void TeleportTo()
    {
        Vector3 playerPos = new Vector3(GvrPointerInputModule.CurrentRaycastResult.worldPosition.x, player.transform.position.y, GvrPointerInputModule.CurrentRaycastResult.worldPosition.z);
        player.transform.position = playerPos;
    }
}
