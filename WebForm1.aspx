<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        body {
            position: relative;
            z-index: 0;
        }
        #divOverlay {
            width: 100%;
            position: absolute;
            top: 50%;
            -webkit-transform: translateY(-50%);
            -ms-transform: translateY(-50%);
            transform: translateY(-50%);
            background-color: orange;
            z-index: 5;
            font-family: Verdana, Geneva, Tahoma, Sans-Serif;
            text-align: center;
            border: 2px solid red;
            border-radius: 5px;
            padding: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divOverlay" runat="server" visible="false">
            <asp:Panel ID="overPanelTop" runat="server"></asp:Panel>
            <asp:Label ID="overLabel" runat="server" Text='Test' />
            <asp:Panel ID="overPanelBottom" runat="server"></asp:Panel>
        </div>
        <div style="font-family: Verdana, Geneva, Tahoma, Sans-Serif; font-size: x-large; text-decoration: underline;">
            Memory by JanSch<br />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        </div>
        <div style="font-family: Verdana, Geneva, Tahoma, Sans-Serif; font-size: large;">
            <asp:Label ID="Textline1" runat="server" Text='' /><br />
            <asp:Label ID="Textline2" runat="server" Text='' /><br />
            <asp:Panel ID="Menue" runat="server"></asp:Panel>
        </div>
        <br />
        <div style="font-family: Verdana, Geneva, Tahoma, Sans-Serif; font-size: medium">
            <asp:Panel ID="myPanel" runat="server"></asp:Panel>
            <asp:Label ID="Points00" Text="Spieler 1 Points: " visible="false" runat="server" /><asp:Label ID="lblPoints00" Text="0" visible="false" runat="server" /><br />
            <asp:Label ID="Points01" Text="Spieler 1 Fehlversuche: " visible="false" runat="server" /><asp:Label ID="lblPoints01" Text="0" visible="false" runat="server" /> <br /><br />
            <asp:Label ID="Points10" Text="Spieler 2 Points: " visible="false" runat="server" /><asp:Label ID="lblPoints10" Text="0" visible="false" runat="server" /><br />
            <asp:Label ID="Points11" Text="Spieler 2 Fehlversuche: " visible="false" runat="server" /><asp:Label ID="lblPoints11" Text="0" visible="false" runat="server" /><br />
            <asp:Label ID="lblMessage2" runat="server" />
        </div>
        <asp:Button ID="reset" Text="Reset" OnClick="ResetBtn_Click" visible="false" runat="server" />
        <asp:Timer ID="Timer1" Enabled="False" Interval="750" OnTick="Timer1_Tick" runat="server">
        </asp:Timer>
    </form>
    </body>
</html>
