﻿namespace RemoteInterfaces
{
    public interface IRemoteCall
    {
        int Foo(string name, Arg1[] arg1s);
        string Echo(string text);
    }

    public interface IRemoteCall1
    {
        int Foo(string name, Arg1[] arg1s);
        string Echo(string text);
    }
}
