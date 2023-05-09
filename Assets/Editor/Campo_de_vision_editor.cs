using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Campo_de_vision))]
public class Campo_de_vision_editor : Editor
{
    private void OnSceneGUI()
    {
        Campo_de_vision CDV = (Campo_de_vision)target; // configurar la vision
        Handles.color = Color.black; // color del rango
        Handles.DrawWireArc(CDV.transform.position, Vector3.up, Vector3.forward, 360, CDV.radio); // dibuja el cono de vision

        //
        Vector3 vista_angulo01 = direccion_angulo(CDV.transform.eulerAngles.y, -CDV.angulo / 2);
        Vector3 vista_angulo02 = direccion_angulo(CDV.transform.eulerAngles.y,  CDV.angulo / 2);

        Handles.color = Color.black; // color del los angulos (rango de los costados)
        Handles.DrawLine(CDV.transform.position, CDV.transform.position + vista_angulo01 * CDV.radio);
        Handles.DrawLine(CDV.transform.position, CDV.transform.position + vista_angulo02 * CDV.radio);

        //si puede ver al jugador
        if (CDV.ver_Player)
        {
            Handles.color = Color.red;
            Handles.DrawLine(CDV.transform.position, CDV.Referencia.transform.position);
        }
        
    }

    private Vector3 direccion_angulo(float eulerY, float angulo_en_grados)
    {
        angulo_en_grados += eulerY;

        return new Vector3(Mathf.Sin(angulo_en_grados * Mathf.Deg2Rad), 0, Mathf.Cos(angulo_en_grados * Mathf.Deg2Rad));
    }
}