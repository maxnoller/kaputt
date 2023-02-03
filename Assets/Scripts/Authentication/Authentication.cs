using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine;

namespace NOBRAIN.KAPUTT.Authentication{

[Serializable]
public class LoginData
{
    public string username;
    public string password;

    public override string ToString(){
        return JsonUtility.ToJson (this, true);
    }
}

public class Authentication
{
    public static Authentication Instance {get {return instance;}}
    private static Authentication instance;
    public Authentication()
    {
        instance = this;
    }

    public string PlayerId(){
        throw new NotImplementedException();
    }

}
}