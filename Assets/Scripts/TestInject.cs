using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Unity.Netcode;

public class TestInject : LifetimeScope
{
    [SerializeField] MainMenu mainMenu;
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        builder.RegisterComponent(mainMenu);
    }
}
