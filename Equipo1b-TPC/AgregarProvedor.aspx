<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="AgregarProvedor.aspx.cs" Inherits="Equipo1b_TPC.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center mt-4">
    <div class="card p-4 border-1 text-white" style="min-width:400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
        <h1 class="textCenter mb-4 tex-primary fw-bold"> Agregar Provedor</h1>
        <div class="mb-3">
            <asp:Label ID="lblNombre" CssClass="form-label" runat="server" Text="Nombre del Provedor"></asp:Label>
            <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="ingrese el nombre" runat="server"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label ID="lblApellido" CssClass="form-label" runat="server" Text="Apellido del Provedor"></asp:Label>
            <asp:TextBox ID="txtApellido" CssClass="form-control" placeholder="Ingrese el apellido" runat="server"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label ID="lblDni"  CssClass="form-label" runat="server" Text="Dni del Provedor"></asp:Label>
            <asp:TextBox ID="txtDni" CssClass="form-control" placeHolder="Ingrese el dni" runat="server"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label ID="lblEmail" CssClass="form-label"  runat="server" Text="Email del Provedor"></asp:Label>
            <asp:TextBox ID="txtEmail" TextMode="Email" CssClass="form-control" placeHolder="ejemplo@correo.com"  runat="server"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label ID="lblTelefono" runat="server" Text="Telefono del Provedor"></asp:Label>
            <asp:TextBox ID="txtTelefono" type="tel" placeHolder="Ej: 1123456789" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label ID="lblDireccion" CssClass="form-label" runat="server" Text="Direccion del Provedor"></asp:Label>
            <asp:TextBox ID="txtDireccion" CssClass="form-control" placeHolder="Ingrese la direccion"  runat="server"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-between">
            <asp:Button ID="btnAgregar" CssClass="btn btn-primary" runat="server" OnClick="btnAgregar_Click" Text="Agregar nuevo Provedor"/>
            <asp:Button ID="btnCancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" runat="server" Text="Cancelar alta de provedor" />
        </div>
    </div>
</div>
</asp:Content>
