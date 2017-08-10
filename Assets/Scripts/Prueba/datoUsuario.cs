using UnityEngine;
using System.Collections;

[System.Serializable]
public class datoUsuario {

    private float usu_cuenta;
    private string usu_nombre;
    private string usu_correo;
    private float usu_valor;
    private string usu_ciudad;
    private string usu_telefono;

    public datoUsuario()
    {
        
    }

    public void setCuenta(float usu_cuenta)
    {
        this.usu_cuenta = usu_cuenta;
    }

    public float getCuenta()
    {
        return this.usu_cuenta;
    }

    public void setNombre(string usu_nombre)
    {
        this.usu_nombre = usu_nombre;
    }

    public string getNombre()
    {
        return this.usu_nombre;
    }

    public void setCorreo(string usu_correo)
    {
        this.usu_correo = usu_correo;
    }

    public string getCorreo()
    {
        return this.usu_correo;
    }

    public void setValor(float usu_valor)
    {
        this.usu_valor = usu_valor;
    }

    public float getValor()
    {
        return this.usu_valor;
    }

    public void setCiudad(string usu_ciudad)
    {
        this.usu_ciudad = usu_ciudad;
    }

    public string getCiudad()
    {
        return this.usu_ciudad;
    }

    public void setTelefono(string usu_telefono)
    {
        this.usu_telefono = usu_telefono;
    }

    public string getTelefono()
    {
        return this.usu_telefono;
    }
}
