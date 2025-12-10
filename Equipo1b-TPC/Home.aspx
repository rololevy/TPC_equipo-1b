<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Equipo1b_TPC.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-5">
        <div class="row g-4 mt-2">
            <!-- VENDEDOR -->
            <div class="col-12 col-md-4" runat="server" id="cardVentas">
                <div class="card h-100">
                    <asp:Image ID="imgVenta" runat="server" CssClass="card-img-top" ImageUrl="~/Images/Icons/venta.png" AlternateText="Ventas" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Ventas</h5>
                        <p class="card-text text-center">Acceso al sistema de ventas, gestión de tickets y registro de operaciones diarias.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnVentas" OnClick="btnVentas_Click" CssClass="btn btn-primary" runat="server" Text="Ir a ventas" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- VENDEDOR -->
            <div class="col-12 col-md-4" runat="server" id="cardResumenVenta">
                <div class="card h-100">
                    <asp:Image ID="imgResumenVenta" CssClass="card-img-top" ImageUrl="~/Images/Icons/ResumenVenta.png" AlternateText="Resumen de venta" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Resumen de venta</h5>
                        <p class="card-text">Consulta del resumen de ventas, gestión de cierres de caja y detalle de medios de pago.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnResumenVenta" CssClass="btn btn-primary" OnClick="btnResumenVenta_Click" runat="server" Text="Ir a Resumen de venta" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- ADMIN -->
            <div class="col-12 col-md-4" runat="server" id="cardProvedores">
                <div class="card h-100">
                    <asp:Image ID="imgProvedores" CssClass="card-img-top" ImageUrl="~/Images/Icons/proveedores.png" AlternateText="Provedores" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Provedores</h5>
                        <p class="card-text">Acceso a compras registra nuevas compras a proveedores existentes.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnProvedores" CssClass="btn btn-primary" runat="server" Text="ir a provedores" OnClick="btnProvedores_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row g-4 mt-1">
            <!-- ADMIN -->
            <div class="col-12 col-md-4" runat="server" id="cardGestionClientes">
                <div class="card h-100">
                    <asp:Image ID="imgGestionClientes" CssClass="card-img-top" ImageUrl="~/Images/Icons/gestionClientes.png" AlternateText="Gestion de clientes" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Gestion de clientes</h5>
                        <p class="card-text">Acceso a la administración de clientes, incluyendo altas, bajas y actualizaciones de datos.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnGestionClientes" CssClass="btn btn-primary" OnClick="btnGestionClientes_Click" runat="server" Text="Ir a gestion de clientes" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- ADMIN -->
            <div class="col-12 col-md-4" runat="server" id="cardArticulos">
                <div class="card h-100">
                    <asp:Image ID="imgArticulos" CssClass="card-img-top" ImageUrl="~/Images/Icons/gestionArticulos.png" AlternateText="Gestion Articulos" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Gestion Articulos</h5>
                        <p class="card-text">Acceso a la gestión de productos, marcas, categorías y control del catálogo disponible.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnArticulos" CssClass="btn btn-primary" runat="server" Text="Ir a Gestión de productos" OnClientClick="window.location='GestionProductos.aspx'; return false;" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- ADMIN -->
            <div class="col-12 col-md-4" runat="server" id="cardStock">
                <div class="card h-100">
                    <asp:Image ID="imgStock" CssClass="card-img-top" ImageUrl="~/Images/Icons/gestionStock.png" AlternateText="Stock" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Gestion de stock</h5>
                        <p class="card-text text-center">Acceso a la gestión de stock consulta de productos disponibles y actualiza sus cantidades.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnStock" OnClick="btnStock_Click" CssClass="btn btn-primary" runat="server" Text="Ir a gestion de stock" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row g-4 mt-1">
            <!-- ADMIN -->
            <div class="col-12 col-md-4" runat="server" id="cardGestionProveedores">
                <div class="card h-100">
                    <asp:Image ID="ImgGestionProveedores" CssClass="card-img-top" ImageUrl="~/Images/Icons/gestionProveedores.png" AlternateText="Gestion de Proveedores" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Gestion de Proveedores</h5>
                        <p class="card-text">Acceso a la administración de proveedores, incluyendo altas, bajas y actualizaciones de datos.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnGestionProveedores" CssClass="btn btn-primary" OnClick="btnGestionProveedores_Click" runat="server" Text="Ir a gestion de Proveedores" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- ADMIN -->
            <div class="col-12 col-md-4" runat="server" id="cardHistorialCompras">
                <div class="card h-100">
                    <asp:Image ID="imgHistorialCompras" CssClass="card-img-top" ImageUrl="~/Images/Icons/HistorialCompras.png" AlternateText="Gestion de Compras" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Historial compras</h5>
                        <p class="card-text">Acceso al historial de compras y visualización del detalle de compras de proveedores activos.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnHistorialCompras" CssClass="btn btn-primary" OnClick="btnHistorialCompras_Click" runat="server" Text="Ir a historial de compras" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
