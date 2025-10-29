<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="gestionProductos.aspx.cs" Inherits="Equipo1b_TPC.gestionArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-group">
        <div class="card">
            <asp:Image ID="imgGestionProductos" CssClass="card-img-top" ImageUrl="https://cdn-icons-png.flaticon.com/512/10608/10608872.png" AlternateText="Gestion de productos" runat="server" />
            <div class="card-body">
                <h5 class="card-title">Gestion de productos</h5>
                <p class="card-text">Gestiona tus productos! Crea nuevos o modifica los existentes</p>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnAgregarProductos" CssClass="btn btn-primary" runat="server" Text="Agregar" />
                    <asp:Button ID="btnModificarProductos" CssClass="btn btn-primary" runat="server" Text="Modificar" />
                </div>
            </div>
        </div>
        <div class="card">
            <asp:Image ID="imgGestionCategorias" CssClass="card-img-top" ImageUrl="https://e7.pngegg.com/pngimages/777/828/png-clipart-computer-icons-symbol-add-to-cart-button-miscellaneous-text.png" AlternateText="Gestion de categorias" runat="server" />
            <div class="card-body">
                <h5 class="card-title">Gestion de categoorias</h5>
                <p class="card-text">Gestion de categorias! Crea nuevas categorias o modifica las existentes.</p>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnGestionCategorias" CssClass="btn btn-primary" OnClick="btnGestionCategorias_Click" runat="server" Text="Gestion de categorias" />
                </div>
            </div>
        </div>
        <div class="card">
            <asp:Image ID="imgMarcas" CssClass="card-img-top" ImageUrl="https://previews.123rf.com/images/iconsdom/iconsdom2011/iconsdom201100983/158909941-brand-management-idea-icon-linear-isolated-illustration-thin-line-vector-web-design-sign-outline.jpg" AlternateText="Gestion de marcas" runat="server" />
            <div class="card-body">
                <h5 class="card-title">Gestion de marcas</h5>
                <p class="card-text">Gestion de marcas!Crea nuevas categorias o modifica las existentes.</p>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnGestionMarcas" CssClass="btn btn-primary" OnClick="btnGestionMarcas_Click" runat="server" Text="Gestion de marcas" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
