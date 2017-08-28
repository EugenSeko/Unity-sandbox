using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {

    [SerializeField] private Transform target;//сериализованная ссылка на объект, вокруг которого производится облет.

    public float rotSpeed = 1.5f;

    private float _rotY;
    private Vector3 _offset;


	void Start () {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;//сохранение начального смещения между камерой и целью.
	}
	
	void LateUpdate () {
        float horInput = Input.GetAxis("Horizontal");
        if (horInput != 0)//медленный поворот камеры при помощи клавиш со стрелками...
        {
            _rotY += horInput * rotSpeed;
        }
        else//или быстрый повор с помощью мыши.
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        }
        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);//преобразование угла поворота в кватернион.
        transform.position = target.position - (rotation * _offset);//поддерживаем начальное смещение, сдвигаемое в соответствии с поворотом камеры.
        transform.LookAt(target);//камера всегда направлена на цель, где бы  относительно этой цели она не распологалась.
	}
}
