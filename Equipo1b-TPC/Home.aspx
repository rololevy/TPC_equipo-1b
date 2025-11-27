<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Equipo1b_TPC.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-5">
        <div class="row g-4 mt-2">
            <div class="col-12 col-md-4">
                <div class="card h-100">
                    <asp:Image ID="imgVenta" runat="server" CssClass="card-img-top" ImageUrl="https://www.ceupe.com/images/easyblog_articles/3939/b2ap3_amp_venta.png" AlternateText="Ventas" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Ventas</h5>
                        <p class="card-text text-center">Ingreso al sistema de ventas.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnVentas" OnClick="btnVentas_Click" CssClass="btn btn-primary" runat="server" Text="Ir a ventas" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-4">
                <div class="card h-100">
                    <asp:Image ID="imgStock" CssClass="card-img-top" ImageUrl="https://cdn-icons-png.freepik.com/512/5166/5166970.png" AlternateText="Stock" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Gestion de stock</h5>
                        <p class="card-text text-center">Acesso a la gestion de stock, ver productos disponibles y modificar stock.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnStock" OnClick="btnStock_Click" CssClass="btn btn-primary" runat="server" Text="Ir a stock" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-4">
                <div class="card h-100">
                    <asp:Image ID="imgProvedores" CssClass="card-img-top" ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQdsTvip-xGcAc0rXklMYadeDmDEVz49bw__Q&s" AlternateText="Provedores" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Provedores</h5>
                        <p class="card-text">Aceso a provedores realizar pedidos  y dar de alta nuevos provedores.</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnProvedores" CssClass="btn btn-primary" runat="server" Text="ir a provedores" OnClick="btnProvedores_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row g-4 mt-1">
            <div class="col-12 col-md-4">
                <div class="card h-100">
                    <asp:Image ID="imgArticulos" CssClass="card-img-top" ImageUrl="https://cdn-icons-png.flaticon.com/512/937/937486.png" AlternateText="Gestion Articulos" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Gestion Articulos</h5>
                        <p class="card-text">Aceso a gestion de articulos,marcas y categorias</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnArticulos" CssClass="btn btn-primary" runat="server" Text="Gestión de productos" OnClientClick="window.location='GestionProductos.aspx'; return false;" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-4">
                <div class="card h-100">
                    <asp:Image ID="imgResumenVenta" CssClass="card-img-top" ImageUrl="https://cdn-icons-png.flaticon.com/512/5070/5070804.png" AlternateText="Resumen de venta" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Resumen de venta</h5>
                        <p class="card-text">Aceso a resumen de venta,consulta de cierre de caja medios de pago</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnResumenVenta" CssClass="btn btn-primary" OnClick="btnResumenVenta_Click" runat="server" Text="Ir a Resumen de venta" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-4">
                <div class="card h-100">
                    <asp:Image ID="imgGestionClientes" CssClass="card-img-top" ImageUrl="https://img.freepik.com/vector-premium/imagen-vectorial-icono-gestion-relaciones-cliente-puede-utilizar-tecnologia-marketing_120816-124918.jpg" AlternateText="Gestion de clientes" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Gestion de clientes</h5>
                        <p class="card-text">Aceso a Gestion de clientes, alta y baja de clientes activos</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnGestionClientes" CssClass="btn btn-primary" OnClick="btnGestionClientes_Click" runat="server" Text="Ir a gestion de clientes" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="row g-4 mt-1">
            <div class="col-12 col-md-4">
                <div class="card h-100">
                    <asp:Image ID="ImgGestionProveedores" CssClass="card-img-top" ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSDHL6EyYPbGo7Gsr1Tgxxce3qlxkLNBqZR7g&sg" AlternateText="Gestion de Proveedores" runat="server" />
                    <div class="card-body text-center">
                        <h5 class="card-title">Gestion de Proveedores</h5>
                        <p class="card-text">Aceso a Gestion de Proveedores,alta y baja de provedores activos</p>
                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnGestionProveedores" CssClass="btn btn-primary" OnClick="btnGestionProveedores_Click" runat="server" Text="Ir a gestion de Proveedores" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
