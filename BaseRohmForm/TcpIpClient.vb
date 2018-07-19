Imports System.Net.Sockets
Imports System.Net
Imports System.Threading
Imports System.Text

Public Class TcpIpClient

    Dim strMessage As String
    Dim myConnect As Socket
    Dim bteAccept(65536) As Byte
    Dim bteSend(65536) As Byte

    Dim Control As String
    Dim PortNumx As String
    Dim Ipx As String
    Dim datax As String
    Public DataType As Boolean = True

    Event TcpStatusConnect(ByVal sts As String)
    Event RcvData(ByVal data As String)
    Public Sub Listener_Click(ByVal PortNum As String, ByVal Ip As String)
        PortNumx = PortNum
        Ipx = Ip
        Dim myThread As New Thread(New ThreadStart(AddressOf StartListen))
        myThread.Start()
    End Sub

    Sub Receive_Click()
        Dim myThread As New Thread(New ThreadStart(AddressOf ReceiveStart))
        myThread.Start()

    End Sub

    Public Sub btnSend_Click(ByVal Data As String)
        datax = Data
        Dim myThread As New Thread(New ThreadStart(AddressOf SendStart))
        myThread.Start()
    End Sub
    
    Public Sub Disconnent()
        If myConnect IsNot Nothing Then
            myConnect.Close()
        End If

    End Sub

    ' Connect :  
    '1. Define IPEndPoint )ประกอบด้วย IP ,Port
    '2. Define Socket ส่วนใหญเหมือนกัน รายระเอียดแต่ละตัวดูใน Help
    '3. Begining Connect ประกอบด้วย IPEndPoint ,CallBack Address,Socket จะมีการตอบสนองอัตโนมัติเมื่อมี
    'Reponse จาก Server Side ที่เปิดรอไว้
    '4. CallBack จะ return ค่า  IAsyncResult เรียก method Socket.Endconnect() เพื่อจบการทำงานที่ค้างอยู่

    Private Sub StartListen()
        Dim intPort As Integer
        Dim bidEndPoint As IPEndPoint

        intPort = Integer.Parse(PortNumx)
        bidEndPoint = New IPEndPoint(IPAddress.Parse(Ipx), intPort)
        myConnect = New Socket _
                     (AddressFamily.InterNetwork, _
                     SocketType.Stream, _
                     ProtocolType.Tcp)

        Try

            Dim myAsyncCallBack As New AsyncCallback(AddressOf ConnectEnd)
            myConnect.BeginConnect(bidEndPoint, myAsyncCallBack, myConnect)
            'Control = "StartLisen"
            'AccessControl()                                                                                                                                   'แก้ปัญหา Cross Thread Error| Cross thread operation not valid: Control "XXXXXXXXXX" accessed from a thread other than the thread it was created. 
            RaiseEvent TcpStatusConnect("Wait")
        Catch ex As SocketException
            Console.WriteLine(ex.Message)

        End Try
    End Sub

    Private Sub ConnectEnd(ByVal pIAsyncResult As IAsyncResult)
        Try
            myConnect.EndConnect(pIAsyncResult)
            'Control = "Successful"
            'AccessControl()
            RaiseEvent TcpStatusConnect("Connected")
            Receive_Click()
        Catch ex As Exception
            myConnect.Close()
            RaiseEvent TcpStatusConnect("Disconnect")
        End Try
       
    End Sub
    'Receive 
    '1. Define receive buffer(Byte)
    '2.Define call back Address
    '3. Call BeginReceive Method
    '4. เมื่อ Auto Reponse มาที่ CallBack Address เรียก method SOCKET.End Receive()เพื่อรับค่าข้อมูลเข้ามา
    ' ที่ receive buffer
    '5. แปลงข้อมูลจาก binary เป็น string
    Private Sub ReceiveStart()
        Try
        Dim myAsyncCallBack As New AsyncCallback(AddressOf ReceiveData)

        myConnect.BeginReceive _
          (bteAccept, 0, 65536, 0, _
          myAsyncCallBack, myConnect)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReceiveData(ByVal pIAsyncResult As IAsyncResult)
        Try
            Dim intByte As Integer
            intByte = myConnect.EndReceive(pIAsyncResult)
            If intByte > 0 Then                                                                                                                                   'Receive data lengh , if not connect =0
                If DataType Then
                    'For i = 0 To intByte
                    '    If bteAccept(i) = 0 Then
                    '        bteAccept(i) = 32
                    '    End If
                    'Next
                    strMessage = Encoding.ASCII.GetString(bteAccept, 0, intByte)
                    strMessage = strMessage.Replace(Chr(0), " "c)                'Change Null Charactor to Space
                Else
                    Dim bytes(intByte - 1) As Byte
                    Array.Copy(bteAccept, bytes, intByte)
                    strMessage = ByteToHex(bytes)
                End If


                Try
                    RaiseEvent RcvData(strMessage)       'if even err this sub err
                Catch ex As Exception

                End Try

            End If
            If ReadContinue Then

                Dim myAsyncCallBack As New AsyncCallback(AddressOf ReceiveData)
                myConnect.BeginReceive(bteAccept, 0, 65536, 0, myAsyncCallBack, myConnect)

            Else
                myConnect.Close()
                RaiseEvent TcpStatusConnect("Disconnect")
            End If

        Catch ex As Exception
            myConnect.Close()
            RaiseEvent TcpStatusConnect("Disconnect")
        End Try
      
    End Sub

    Private Sub SendStart()
        Dim myAsyncCallBack As New AsyncCallback(AddressOf SendData)
        If DataType Then

            bteSend = Encoding.ASCII.GetBytes(datax)
        Else
            bteSend = HexToByte(datax)
        End If


            myConnect.BeginSend(bteSend, 0, bteSend.Length, _
          SocketFlags.DontRoute, myAsyncCallBack, myConnect)

    End Sub

    Private Sub SendData(ByVal pIAsyncResult As IAsyncResult)
        Dim intSend As Integer
        intSend = myConnect.EndSend(pIAsyncResult)
        'strMessage = vbCrLf + "Message sent" + intSend.ToString + " bytes "

        'strMessage = ""
        datax = ""

    End Sub

  
    Public Function GetLocalIP() As String
        Dim IPList As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)
        For Each IPaddress In IPList.AddressList
            'Only return IPv4 routable IPs
            If (IPaddress.AddressFamily = Sockets.AddressFamily.InterNetwork) AndAlso (Not IsPrivateIP(IPaddress.ToString)) Then
                Return IPaddress.ToString
            End If
        Next
        Return ""
    End Function
    Function IsPrivateIP(ByVal CheckIP As String) As Boolean
        Dim Quad1, Quad2 As Integer

        Quad1 = CInt(CheckIP.Substring(0, CheckIP.IndexOf(".")))
        Quad2 = CInt(CheckIP.Substring(CheckIP.IndexOf(".") + 1).Substring(0, CheckIP.IndexOf(".")))
        Select Case Quad1
            Case 10
                'Return True
            Case 172
                'If Quad2 >= 16 And Quad2 <= 31 Then Return True
            Case 192
                If Quad2 = 168 Then Return True
        End Select
        Return False
    End Function
    Private Function ByteToHex(ByVal comByte As Byte()) As String

        'create a new StringBuilder object

        Dim builder As New System.Text.StringBuilder(comByte.Length * 3)
       
        'loop through each byte in the array
        For Each data As Byte In comByte
            builder.Append(System.Convert.ToString(data, 16).PadLeft(2, "0"c).PadRight(3, " "c))
            'convert the byte to a string and add to the stringbuilder
        Next
        'return the converted value
        Return builder.ToString().ToUpper()

    End Function
    Private Function HexToByte(ByVal msg As String) As Byte()
        Dim tmpMsg As Integer = msg.Length Mod 2
        If tmpMsg = 1 Then
            msg = msg + " "
        End If
        If msg.Length Mod 2 = 0 Then
            'remove any spaces from the string
            Dim _msg As String = msg
            _msg = msg.Replace(" ", "")
            'create a byte array the length of the
            'divided by 2 (Hex is 2 characters in length)
            Dim comBuffer As Byte() = New Byte(CInt(_msg.Length / 2 - 1)) {}
            For i As Integer = 0 To _msg.Length - 1 Step 2
                comBuffer(CInt(i / 2)) = CByte(System.Convert.ToByte(_msg.Substring(i, 2), 16))
            Next

            'loop through the length of the provided string
            'convert each set of 2 characters to a byte
            'and add to the array
            'return the array
            Return comBuffer
        Else
            Return Nothing
        End If
    End Function
    Private _ReadContinue As Boolean = True
    Public Property ReadContinue() As Boolean
        Get
            Return _ReadContinue
        End Get
        Set(ByVal value As Boolean)
            _ReadContinue = value
        End Set
    End Property



    



End Class
