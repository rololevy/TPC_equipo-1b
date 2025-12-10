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
    <!-- Panel con la grilla -->
    <asp:Panel ID="pnlGestionProductos" runat="server" Visible="false">
        <div class="container mt-4">
            <div class="card">
                <div class="card-header bg-primary text-white">
                    <h4 class="mb-0">Listado de Productos</h4>
                </div>
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <asp:TextBox ID="txtBuscar" CssClass="form-control" runat="server" placeholder="Buscar producto..."></asp:TextBox>
                        <asp:Button ID="btnNuevoProducto" CssClass="btn btn-success ml-2" runat="server" Text="+ Nuevo Producto" />
                    </div>

                    <asp:GridView ID="gvProductos" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="Id" />
                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                            <asp:BoundField HeaderText="Marca" DataField="Marca" />
                            <asp:BoundField HeaderText="Categoría" DataField="Categoria" />
                            <asp:BoundField HeaderText="Precio Compra" DataField="PrecioCompra" DataFormatString="{0:C}" />
                            <asp:BoundField HeaderText="% Ganancia" DataField="PorcentajeGanancia" />
                            <asp:BoundField HeaderText="Stock Actual" DataField="StockActual" />
                            <asp:BoundField HeaderText="Stock Mínimo" DataField="StockMinimo" />
                            <asp:TemplateField HeaderText="Activo">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# (bool)Eval("Activo") ? "Sí" : "No" %>' CssClass="badge badge-secondary"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-primary mr-1" Text="Editar" />
                                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger" Text="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info">No hay productos para mostrar</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
