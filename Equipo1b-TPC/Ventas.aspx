<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="Equipo1b_TPC.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-6">
                <asp:UpdatePanel ID="upClientes" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtFiltrarClientes" AutoPostBack="true" OnTextChanged="txtFiltrarClientes_TextChanged" placeHolder="Buscar Cliente ......" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="ddlClientes" CssClass="form-select w-75" runat="server"></asp:DropDownList>
                        <asp:Button ID="btnAgregarCliente" runat="server" CssClass="btn btn-primary" Text="Button" />
                    </ContentTemplate>

                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</asp:Content>

