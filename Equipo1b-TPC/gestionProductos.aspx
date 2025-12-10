<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="gestionProductos.aspx.cs" Inherits="Equipo1b_TPC.gestionArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-5">
        <div class="row g-4 mt-2">
            <div class="col-12 col-md-4">
                <div class="card h-100">
                    <asp:Image ID="imgGestionProductos" CssClass="card-img-top" ImageUrl="~/Images/Icons/GestionProductos.png" AlternateText="Gestion de productos" runat="server" />
                    <div class="card-body">
                        <h5 class="card-title">Gestion de productos</h5>
                        <p class="card-text">Acceso a la gestión de productos crea nuevos o modifica los existentes.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnGestionProductos" CssClass="btn btn-primary" OnClick="btnGestionProductos_Click" runat="server" Text="Gestión de productos" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-4">
                <div class="card h-100">
                    <asp:Image ID="imgGestionCategorias" CssClass="card-img-top" ImageUrl="~/Images/Icons/GestionCategorias.png" AlternateText="Gestion de categorias" runat="server" />
                    <div class="card-body">
                        <h5 class="card-title">Gestion de categorias</h5>
                        <p class="card-text">Acceso a la gestión de categorías crea nuevas o modifica las existentes.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnGestionCategorias" CssClass="btn btn-primary" OnClick="btnGestionCategorias_Click" runat="server" Text="Gestion de categorias" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-4">
                <div class="card">
                    <asp:Image ID="imgMarcas" CssClass="card-img-top" ImageUrl="~/Images/Icons/GestionMarcas.png" AlternateText="Gestion de marcas" runat="server" />
                    <div class="card-body">
                        <h5 class="card-title">Gestion de marcas</h5>
                        <p class="card-text">Acceso a la Gestion de marcas Crea nuevas categorias o modifica las existentes.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnGestionMarcas" CssClass="btn btn-primary" OnClick="btnGestionMarcas_Click" runat="server" Text="Gestion de marcas" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
