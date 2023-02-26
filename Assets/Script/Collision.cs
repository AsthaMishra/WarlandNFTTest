using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Debug.Log("OnColision");

        try
        {
            if (collision.gameObject == null)
                return;

            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("OnColision");
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception");
        }
    }

    private void OnCollisionStay(UnityEngine.Collision collision)
    {
        Debug.Log("OnColision");

        try
        {
            if (collision.gameObject == null)
                return;

            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("OnColision");
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception");
        }
    }

    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        Debug.Log("OnColision");

        try
        {
            if (collision.gameObject == null)
                return;

            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("OnColision");
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception");
        }
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "Sol" || col.gameObject.tag == "Plateforme")
    //    {
    //        Physics.Tri(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());

    //    }
    //}

}