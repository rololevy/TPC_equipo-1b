<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="AgregarCliente.aspx.cs" Inherits="Equipo1b_TPC.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center mt-4">
        <div class="card p-4 border-1 text-white" style="min-width:400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
            <asp:Label ID="lblTitulo" ClientIDMode="Static" CssClass="h1 text-center text-white fw-bold text-primary" runat="server" Text="Agregar Cliente"></asp:Label>
            <div class="mb-3">
                <asp:Label ID="lblrazonSocial" CssClass="form-label" runat="server" Text="Razon social del cliente"></asp:Label>
                <asp:TextBox ID="txtRazonSocial" ClientIDMode="Static"  CssClass="form-control" placeholder="Ingrese la razon social" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblCuit"  CssClass="form-label" runat="server" Text="Cuit del cliente"></asp:Label>
                <asp:TextBox ID="txtCuit" ClientIDMode="Static"   CssClass="form-control" placeHolder="Ingrese el cuit" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblEmail" CssClass="form-label"  runat="server" Text="Email del cliente"></asp:Label>
                <asp:TextBox ID="txtEmail" ClientIDMode="Static"  TextMode="Email" CssClass="form-control" placeHolder="ejemplo@correo.com"  runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblTelefono" runat="server" Text="Telefono del cliente"></asp:Label>
                <asp:TextBox ID="txtTelefono" ClientIDMode="Static"  type="tel" placeHolder="Ej: 1123456789" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblDireccion" CssClass="form-label" runat="server" Text="Direccion del cliente"></asp:Label>
                <asp:TextBox ID="txtDireccion" ClientIDMode="Static"  CssClass="form-control" placeHolder="Ingrese la direccion"  runat="server"></asp:TextBox>
            </div>
            <div class="d-flex justify-content-between gap-1">
                <asp:Button ID="btnAgregar" CssClass="btn btn-primary" runat="server" OnClick="btnAgregar_Click" Text="Agregar nuevo cliente" />
                <asp:Button ID="btnCancelar" CssClass="btn btn-danger" OnClick="btnCancelar_Click" runat="server" Text="Cancelar alta de cliente" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblConfirmacion" ClientIDMode="Static" Visible="false" runat="server" Text=""></asp:Label>
            </div>
            
        </div>
    </div>
</asp:Content>
