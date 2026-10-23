using UnityEngine;
using UnityEngine.InputSystem;

public class MainCameraController : MonoBehaviour
{

    public float rotateSpeed = 10.0f, movementSpeed = 10.0f, zoomSpeed = 10.0f, zoomMin = -30f, zoomMax = 40f;
    

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        float rotate = 0f;
        float horizontalMovement = 0f;
        float verticalMovement = 0f;

        float _mult = Keyboard.current.leftShiftKey.isPressed ? 2f : 1f;

        //if (Input.GetKey(KeyCode.Q))
        //{
        //    rotate = -1f;
        //}
        //else if (Input.GetKey(KeyCode.E)) 
        //{
        //    rotate = 1f;
        //}



        // Вращение
        if (Keyboard.current.qKey.isPressed)
        {
            rotate = -1f;
        }
        else if (Keyboard.current.eKey.isPressed) 
        {
            rotate = 1f;
        }

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult, Space.World);

        // Перемещение

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            verticalMovement += 1f;
        }
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            verticalMovement -= 1f;
        }

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            horizontalMovement += 1f;
        }
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            horizontalMovement -= 1f;
        }

        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0f;
        right.Normalize();

        //Vector3 movement = new Vector3(
        //    horizontalMovement,
        //    0f,
        //    verticalMovement
        //);

        Vector3 movement = forward * verticalMovement + right * horizontalMovement;

        if (movement.sqrMagnitude > 1f)
            movement.Normalize();

        //transform.Translate(movement * Time.deltaTime * _mult * movementSpeed, Space.World);
        // Двигаем камеру в направлении её поворота
        transform.position += movement * movementSpeed * Time.deltaTime * _mult;


        // Zoom

        if (Mouse.current != null)
        {
            float scroll = Mouse.current.scroll.ReadValue().y;

            Vector3 scrolling = transform.up * scroll * zoomSpeed * _mult;

            transform.position -= scrolling;
        }
        // Ограничение
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, zoomMin, zoomMax), transform.position.z);

    }

}
