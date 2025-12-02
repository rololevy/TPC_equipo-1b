<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="AgregarCliente.aspx.cs" Inherits="Equipo1b_TPC.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center mt-4">
        <div class="card p-4 border-1 text-white" style="min-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
            <asp:Label ID="lblTitulo" ClientIDMode="Static" CssClass="h1 text-center text-white fw-bold text-primary" runat="server" Text="Agregar Cliente"></asp:Label>
            <div class="mb-3">
                <asp:Label ID="lblrazonSocial" CssClass="form-label" runat="server" Text="Razon social del cliente"></asp:Label>
                <asp:TextBox ID="txtRazonSocial" ClientIDMode="Static" CssClass="form-control" placeholder="Ingrese la razon social" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtRazonSocial" Display="Dynamic" CssClass="text-danger fw-bold" ErrorMessage="La razon Social es obligatoria" runat="server">
                </asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblCuit" CssClass="form-label" runat="server" Text="Cuit del cliente"></asp:Label>
                <asp:TextBox ID="txtCuit" ClientIDMode="Static" MaxLength="11" inputMode="numeric" AutoPostBack="true" CssClass="form-control" placeHolder="Ingrese el cuit" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtCuit" CssClass="text-danger fw-bold" ErrorMessage="El cuit es obligatorio" Display="Dynamic" runat="server"> </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtCuit" ValidationExpression="^\d{11}$" CssClass="text-danger fw-bold" ErrorMessage="Debe ingresar un CUIT valido de 11 numeros" Display="Dynamic" runat="server" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblTipoFactura" CssClass="form-label" runat="server" Text="Tipo de factura"></asp:Label>
                <asp:DropDownList ID="ddlTipoFactura" CssClass="form-select" runat="server">
                    <asp:ListItem Text="Factura B" Value="B"></asp:ListItem>
                    <asp:ListItem Text="Factura A" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Factura C" Value="C"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblEmail" CssClass="form-label" runat="server" Text="Email del cliente"></asp:Label>
                <asp:TextBox ID="txtEmail" ClientIDMode="Static" TextMode="Email" CssClass="form-control" placeHolder="ejemplo@correo.com" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ControlToValidate="txtEmail" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ErrorMessage="Email con formato invalido" CssClass="text-danger fw-bold" Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblTelefono" runat="server" Text="Telefono del cliente"></asp:Label>
                <asp:TextBox ID="txtTelefono" ClientIDMode="Static" TextMode="Phone" placeHolder="Ej: 1123456789" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblDireccion" CssClass="form-label" runat="server" Text="Direccion del cliente"></asp:Label>
                <asp:TextBox ID="txtDireccion" ClientIDMode="Static" CssClass="form-control" placeHolder="Ingrese la direccion" runat="server"></asp:TextBox>
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
