//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Login.proto
namespace Login
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ReqLogin")]
  public partial class ReqLogin : global::ProtoBuf.IExtensible
  {
    public ReqLogin() {}
    
    private string _user = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"user", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string user
    {
      get { return _user; }
      set { _user = value; }
    }
    private string _account = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string account
    {
      get { return _account; }
      set { _account = value; }
    }
    private string _password = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"password", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string password
    {
      get { return _password; }
      set { _password = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"My")]
  public partial class My : global::ProtoBuf.IExtensible
  {
    public My() {}
    
    private string _account = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string account
    {
      get { return _account; }
      set { _account = value; }
    }
    private string _password = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"password", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string password
    {
      get { return _password; }
      set { _password = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}