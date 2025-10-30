<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="productos.aspx.cs" Inherits="Equipo1b_TPC.productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0">Gestión de Productos</h4>
            </div>
            <div class="card-body">
                <p>Esta página muestra la grilla y opciones para listar y gestionar productos (solo front-end demo).</p>
                <asp:TextBox ID="txtBuscarProd" runat="server" CssClass="form-control mb-2" placeholder="Buscar..."></asp:TextBox>
                <asp:Button ID="btnNuevoProd" runat="server" CssClass="btn btn-success mb-3" Text="+ Nuevo Producto" />

                <asp:GridView ID="gvProductosPage" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false">
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
                                <asp:Button ID="btnEditarPage" runat="server" CssClass="btn btn-sm btn-outline-primary mr-1" Text="Editar" />
                                <asp:Button ID="btnEliminarPage" runat="server" CssClass="btn btn-sm btn-outline-danger" Text="Eliminar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>