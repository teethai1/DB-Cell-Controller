﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.1022
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System
Imports System.Runtime.Serialization

Namespace JIGWebService
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="CheckResult", [Namespace]:="http://tempuri.org/"),  _
     System.SerializableAttribute(),  _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(JIGWebService.CheckResultJIGInfo))>  _
    Partial Public Class CheckResult
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        Private IsPassField As Boolean
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private ErrorMessageField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private ItemCodeField As String
        
        <Global.System.ComponentModel.BrowsableAttribute(false)>  _
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)>  _
        Public Property IsPass() As Boolean
            Get
                Return Me.IsPassField
            End Get
            Set
                If (Me.IsPassField.Equals(value) <> true) Then
                    Me.IsPassField = value
                    Me.RaisePropertyChanged("IsPass")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=1)>  _
        Public Property ErrorMessage() As String
            Get
                Return Me.ErrorMessageField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ErrorMessageField, value) <> true) Then
                    Me.ErrorMessageField = value
                    Me.RaisePropertyChanged("ErrorMessage")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=2)>  _
        Public Property ItemCode() As String
            Get
                Return Me.ItemCodeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ItemCodeField, value) <> true) Then
                    Me.ItemCodeField = value
                    Me.RaisePropertyChanged("ItemCode")
                End If
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="CheckResultJIGInfo", [Namespace]:="http://tempuri.org/"),  _
     System.SerializableAttribute()>  _
    Partial Public Class CheckResultJIGInfo
        Inherits JIGWebService.CheckResult
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private JIGInfoField As JIGWebService.JIGInfo
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false)>  _
        Public Property JIGInfo() As JIGWebService.JIGInfo
            Get
                Return Me.JIGInfoField
            End Get
            Set
                If (Object.ReferenceEquals(Me.JIGInfoField, value) <> true) Then
                    Me.JIGInfoField = value
                    Me.RaisePropertyChanged("JIGInfo")
                End If
            End Set
        End Property
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="JIGInfo", [Namespace]:="http://tempuri.org/"),  _
     System.SerializableAttribute()>  _
    Partial Public Class JIGInfo
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        Private IdField As Integer
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private SubTypeField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private TypeField As String
        
        Private LifeTimeField As Integer
        
        Private STDLifeTimeField As Integer
        
        Private PeriodCheckTimeField As Integer
        
        Private STDPeriodCheckTimeField As Integer
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private CodeField As String
        
        <Global.System.ComponentModel.BrowsableAttribute(false)>  _
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)>  _
        Public Property Id() As Integer
            Get
                Return Me.IdField
            End Get
            Set
                If (Me.IdField.Equals(value) <> true) Then
                    Me.IdField = value
                    Me.RaisePropertyChanged("Id")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false)>  _
        Public Property SubType() As String
            Get
                Return Me.SubTypeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.SubTypeField, value) <> true) Then
                    Me.SubTypeField = value
                    Me.RaisePropertyChanged("SubType")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false)>  _
        Public Property Type() As String
            Get
                Return Me.TypeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.TypeField, value) <> true) Then
                    Me.TypeField = value
                    Me.RaisePropertyChanged("Type")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true, Order:=3)>  _
        Public Property LifeTime() As Integer
            Get
                Return Me.LifeTimeField
            End Get
            Set
                If (Me.LifeTimeField.Equals(value) <> true) Then
                    Me.LifeTimeField = value
                    Me.RaisePropertyChanged("LifeTime")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true, Order:=4)>  _
        Public Property STDLifeTime() As Integer
            Get
                Return Me.STDLifeTimeField
            End Get
            Set
                If (Me.STDLifeTimeField.Equals(value) <> true) Then
                    Me.STDLifeTimeField = value
                    Me.RaisePropertyChanged("STDLifeTime")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true, Order:=5)>  _
        Public Property PeriodCheckTime() As Integer
            Get
                Return Me.PeriodCheckTimeField
            End Get
            Set
                If (Me.PeriodCheckTimeField.Equals(value) <> true) Then
                    Me.PeriodCheckTimeField = value
                    Me.RaisePropertyChanged("PeriodCheckTime")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true, Order:=6)>  _
        Public Property STDPeriodCheckTime() As Integer
            Get
                Return Me.STDPeriodCheckTimeField
            End Get
            Set
                If (Me.STDPeriodCheckTimeField.Equals(value) <> true) Then
                    Me.STDPeriodCheckTimeField = value
                    Me.RaisePropertyChanged("STDPeriodCheckTime")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=7)>  _
        Public Property Code() As String
            Get
                Return Me.CodeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.CodeField, value) <> true) Then
                    Me.CodeField = value
                    Me.RaisePropertyChanged("Code")
                End If
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="JIGWebService.JIGServiceSoap")>  _
    Public Interface JIGServiceSoap
        
        'CODEGEN: Generating message contract since element name LotNo from namespace http://tempuri.org/ is not marked nillable
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/SetupCollet", ReplyAction:="*")>  _
        Function SetupCollet(ByVal request As JIGWebService.SetupColletRequest) As JIGWebService.SetupColletResponse
        
        'CODEGEN: Generating message contract since element name LotNo from namespace http://tempuri.org/ is not marked nillable
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/SetupColletWithInfo", ReplyAction:="*")>  _
        Function SetupColletWithInfo(ByVal request As JIGWebService.SetupColletWithInfoRequest) As JIGWebService.SetupColletWithInfoResponse
        
        'CODEGEN: Generating message contract since element name TestResult from namespace http://tempuri.org/ is not marked nillable
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/Test", ReplyAction:="*")>  _
        Function Test(ByVal request As JIGWebService.TestRequest) As JIGWebService.TestResponse
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class SetupColletRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="SetupCollet", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As JIGWebService.SetupColletRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As JIGWebService.SetupColletRequestBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class SetupColletRequestBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public LotNo As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=1)>  _
        Public qrCode As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=2)>  _
        Public machineNo As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=3)>  _
        Public machineType As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=4)>  _
        Public userNo As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal LotNo As String, ByVal qrCode As String, ByVal machineNo As String, ByVal machineType As String, ByVal userNo As String)
            MyBase.New
            Me.LotNo = LotNo
            Me.qrCode = qrCode
            Me.machineNo = machineNo
            Me.machineType = machineType
            Me.userNo = userNo
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class SetupColletResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="SetupColletResponse", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As JIGWebService.SetupColletResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As JIGWebService.SetupColletResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class SetupColletResponseBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public SetupColletResult As JIGWebService.CheckResult
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal SetupColletResult As JIGWebService.CheckResult)
            MyBase.New
            Me.SetupColletResult = SetupColletResult
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class SetupColletWithInfoRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="SetupColletWithInfo", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As JIGWebService.SetupColletWithInfoRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As JIGWebService.SetupColletWithInfoRequestBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class SetupColletWithInfoRequestBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public LotNo As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=1)>  _
        Public qrCode As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=2)>  _
        Public machineNo As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=3)>  _
        Public machineType As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=4)>  _
        Public userNo As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal LotNo As String, ByVal qrCode As String, ByVal machineNo As String, ByVal machineType As String, ByVal userNo As String)
            MyBase.New
            Me.LotNo = LotNo
            Me.qrCode = qrCode
            Me.machineNo = machineNo
            Me.machineType = machineType
            Me.userNo = userNo
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class SetupColletWithInfoResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="SetupColletWithInfoResponse", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As JIGWebService.SetupColletWithInfoResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As JIGWebService.SetupColletWithInfoResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class SetupColletWithInfoResponseBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public SetupColletWithInfoResult As JIGWebService.CheckResultJIGInfo
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal SetupColletWithInfoResult As JIGWebService.CheckResultJIGInfo)
            MyBase.New
            Me.SetupColletWithInfoResult = SetupColletWithInfoResult
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class TestRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="Test", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As JIGWebService.TestRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As JIGWebService.TestRequestBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute()>  _
    Partial Public Class TestRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class TestResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="TestResponse", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As JIGWebService.TestResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As JIGWebService.TestResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class TestResponseBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public TestResult As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal TestResult As String)
            MyBase.New
            Me.TestResult = TestResult
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface JIGServiceSoapChannel
        Inherits JIGWebService.JIGServiceSoap, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class JIGServiceSoapClient
        Inherits System.ServiceModel.ClientBase(Of JIGWebService.JIGServiceSoap)
        Implements JIGWebService.JIGServiceSoap
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function JIGWebService_JIGServiceSoap_SetupCollet(ByVal request As JIGWebService.SetupColletRequest) As JIGWebService.SetupColletResponse Implements JIGWebService.JIGServiceSoap.SetupCollet
            Return MyBase.Channel.SetupCollet(request)
        End Function
        
        Public Function SetupCollet(ByVal LotNo As String, ByVal qrCode As String, ByVal machineNo As String, ByVal machineType As String, ByVal userNo As String) As JIGWebService.CheckResult
            Dim inValue As JIGWebService.SetupColletRequest = New JIGWebService.SetupColletRequest()
            inValue.Body = New JIGWebService.SetupColletRequestBody()
            inValue.Body.LotNo = LotNo
            inValue.Body.qrCode = qrCode
            inValue.Body.machineNo = machineNo
            inValue.Body.machineType = machineType
            inValue.Body.userNo = userNo
            Dim retVal As JIGWebService.SetupColletResponse = CType(Me,JIGWebService.JIGServiceSoap).SetupCollet(inValue)
            Return retVal.Body.SetupColletResult
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function JIGWebService_JIGServiceSoap_SetupColletWithInfo(ByVal request As JIGWebService.SetupColletWithInfoRequest) As JIGWebService.SetupColletWithInfoResponse Implements JIGWebService.JIGServiceSoap.SetupColletWithInfo
            Return MyBase.Channel.SetupColletWithInfo(request)
        End Function
        
        Public Function SetupColletWithInfo(ByVal LotNo As String, ByVal qrCode As String, ByVal machineNo As String, ByVal machineType As String, ByVal userNo As String) As JIGWebService.CheckResultJIGInfo
            Dim inValue As JIGWebService.SetupColletWithInfoRequest = New JIGWebService.SetupColletWithInfoRequest()
            inValue.Body = New JIGWebService.SetupColletWithInfoRequestBody()
            inValue.Body.LotNo = LotNo
            inValue.Body.qrCode = qrCode
            inValue.Body.machineNo = machineNo
            inValue.Body.machineType = machineType
            inValue.Body.userNo = userNo
            Dim retVal As JIGWebService.SetupColletWithInfoResponse = CType(Me,JIGWebService.JIGServiceSoap).SetupColletWithInfo(inValue)
            Return retVal.Body.SetupColletWithInfoResult
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function JIGWebService_JIGServiceSoap_Test(ByVal request As JIGWebService.TestRequest) As JIGWebService.TestResponse Implements JIGWebService.JIGServiceSoap.Test
            Return MyBase.Channel.Test(request)
        End Function
        
        Public Function Test() As String
            Dim inValue As JIGWebService.TestRequest = New JIGWebService.TestRequest()
            inValue.Body = New JIGWebService.TestRequestBody()
            Dim retVal As JIGWebService.TestResponse = CType(Me,JIGWebService.JIGServiceSoap).Test(inValue)
            Return retVal.Body.TestResult
        End Function
    End Class
End Namespace
