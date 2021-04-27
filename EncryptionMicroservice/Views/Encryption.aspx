<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Encryption.aspx.cs" Inherits="EncryptionMicroservice.Views.Encryption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Lookup entry</h3>
    <p>Id:</p>
    <asp:TextBox ID="LookupIdTextBox" runat="server" Height="16px" Width="535px"></asp:TextBox>
    <br />
    <p>Key:</p>
    <asp:TextBox ID="LookupKeyTextBox" runat="server" Height="16px" Width="535px"></asp:TextBox>
    <br />
    <br />
    <p>Output:</p>
    <asp:TextBox ID="LookupOutputTextBox" Enabled="false" runat="server" Height="71px" Width="538px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="LookupButton" runat="server" Text="Lookup" Width="111px" />
    <br />
    <br />
    <hr />
    <br />
    <h3>Add entry</h3>
    <br />
    <p>Input:</p>
    <asp:TextBox ID="AddInputTextBox" runat="server" Height="71px" Width="538px"></asp:TextBox>
    <br />
    <br />
    <p>Key:</p>
    <asp:TextBox ID="AddKeyTextBox" runat="server" Height="16px" Width="535px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="AddEntryResultLabel" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Button ID="AddButton" runat="server" Text="Add" Width="115px" />
    <br />
    <br />
    <hr />
    <br />
    <h3>Edit entry</h3>
    <p>Id:</p>
    <asp:TextBox ID="LookupIdTextBox0" runat="server" Height="16px" Width="535px"></asp:TextBox>
    <br />
    <p>Key:</p>
    <asp:TextBox ID="EditKeyTextBox" runat="server" Height="16px" Width="535px"></asp:TextBox>
    <br />
    <br />
    <p>Input:</p>
    <asp:TextBox ID="EditInputTextBox" runat="server" Height="71px" Width="538px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="EditButton" runat="server" Text="Edit" Width="111px" />
    <br />
    <br />
</asp:Content>
