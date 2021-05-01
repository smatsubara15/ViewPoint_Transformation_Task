using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
//Author: Mengyu Chen, 2018
//For questions: mengyuchenmat@gmail.com
namespace XRTK
{
    public class ControllerStats : MonoBehaviour
    {

        //in order to use trackpadpos and trackpadtouch, you need to bind your controller input with custom names as TouchPad and TouchPos
        // static public Vector2 getTrackPadPos(Hand hand)
        // {
        // 	SteamVR_Action_Vector2 trackpadPos = SteamVR_Actions._default.TouchPos;
        // 	return trackpadPos.GetAxis(hand.handType);
        // }
        // static public bool getTrackPadTouch(Hand hand){
        // 	return SteamVR_Actions._default.TouchPad.GetState(hand.handType);
        // }
        // static public bool getTrackPadTouchDown(Hand hand){
        // 	return SteamVR_Actions._default.TouchPad.GetStateDown(hand.handType);
        // }
        // static public bool getTrackPadTouchUp(Hand hand){
        // 	return SteamVR_Actions._default.TouchPad.GetStateUp(hand.handType);
        // }

        static public bool getPinch(Hand hand)
        {
            return SteamVR_Actions._default.GrabPinch.GetState(hand.handType);
        }

        static public bool getPinchDown(Hand hand)
        {
            /// <summary>Returns true if the value of the action has been set to false (from true) in the previous update.</summary>
            return SteamVR_Actions._default.GrabPinch.GetStateDown(hand.handType);
        }

        static public bool getPinchUp(Hand hand)
        {
            //Returns true if the value of the action is currently true
            return SteamVR_Actions._default.GrabPinch.GetStateUp(hand.handType);
        }

        static public bool getGrip(Hand hand)
        {
            /// <summary>Returns true if the value of the action has been set to false (from true) in the previous update.</summary>
            return SteamVR_Actions._default.GrabGrip.GetState(hand.handType);
        }

        static public bool getGrip_Down(Hand hand)
        {
            /// <summary>Returns true if the value of the action has been set to false (from true) in the previous update.</summary>
            return SteamVR_Actions._default.GrabGrip.GetStateDown(hand.handType);
        }

        static public bool getGrip_Up(Hand hand)
        {
            //Returns true if the value of the action is currently true
            return SteamVR_Actions._default.GrabGrip.GetStateUp(hand.handType);
        }

        // static public bool getMenu(Hand hand)
        // {
        // 	return SteamVR_Actions._default.MenuButton.GetState(hand.handType);
        // }

        // static public bool getMenu_Down(Hand hand)
        // {
        // 	/// <summary>Returns true if the value of the action has been set to false (from true) in the previous update.</summary>
        // 	return SteamVR_Actions._default.MenuButton.GetStateDown(hand.handType);
        // }

        // static public bool getMenu_Up(Hand hand)
        // {
        // 	//Returns true if the value of the action is currently true
        // 	return SteamVR_Actions._default.MenuButton.GetStateUp(hand.handType);
        // }

        static public bool getTouchPad(Hand hand)
        {
            return SteamVR_Actions._default.Teleport.GetState(hand.handType);
        }

        static public bool getTouchPad_Down(Hand hand)
        {
            return SteamVR_Actions._default.Teleport.GetStateDown(hand.handType);
        }

        static public bool getTouchPad_Up(Hand hand)
        {
            //Returns true if the value of the action is currently true
            return SteamVR_Actions._default.Teleport.GetStateUp(hand.handType);
        }

        static public Vector3 getControllerPosition(Hand hand)
        {
            SteamVR_Action_Pose[] poseActions = SteamVR_Actions._default.poseActions;
            if (poseActions.Length > 0)
            {
                return poseActions[0].GetLocalPosition(hand.handType);
            }
            return new Vector3(0, 0, 0);
        }

        static public Quaternion getControllerRotation(Hand hand)
        {
            SteamVR_Action_Pose[] poseActions = SteamVR_Actions._default.poseActions;
            if (poseActions.Length > 0)
            {
                return poseActions[0].GetLocalRotation(hand.handType);
            }
            return Quaternion.identity;
        }

    }
}