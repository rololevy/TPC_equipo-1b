<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="productos.aspx.cs" Inherits="Equipo1b_TPC.productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4 mb-4">
        <div class="card-grid p-4 border-1 text-white" style="min-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
            <h1 class="text-center text-white fw-bold text-primary">Gestion de Productos
            </h1>
            <div class="card-body">
                <!-- Filtros y búsqueda -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <asp:TextBox ID="txtBuscarProd" runat="server" CssClass="form-control"
                            placeholder="Buscar producto..." AutoPostBack="true"
                            OnTextChanged="txtBuscarProd_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnNuevoProd" runat="server" CssClass="btn btn-success w-100"
                            Text="+ Nuevo" OnClick="btnNuevoProd_Click" />
                    </div>
                </div>
                <div class="col-12 tabla-stock">
                    <!-- GridView con funcionalidad -->
                    <asp:GridView ID="gvProductosPage" runat="server" CssClass="table table-striped table-hover"
                        AutoGenerateColumns="false" DataKeyNames="Id"
                        OnRowCommand="gvProductosPage_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="Id" ItemStyle-Width="50px" />
                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                            <asp:BoundField HeaderText="Marca" DataField="Marca.Nombre" />
                            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Nombre" />
                            <asp:BoundField HeaderText="Precio Compra" DataField="PrecioCompra" DataFormatString="{0:C2}" />
                            <asp:BoundField HeaderText="% Ganancia" DataField="PorcentajeGanancia" DataFormatString="{0}%" />
                            <asp:TemplateField HeaderText="Precio Venta">
                                <ItemTemplate>
                                    <%# String.Format("{0:C2}", ((Equipo1b_TPC.Dominio.Producto)Container.DataItem).CalcularPrecioVenta()) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Stock Actual" DataField="StockActual" ItemStyle-CssClass="text-center" />
                            <asp:BoundField HeaderText="Stock Mínimo" DataField="StockMinimo" ItemStyle-CssClass="text-center" />
                            <asp:TemplateField HeaderText="Estado Stock">
                                <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("StockActual")) == 0
                                    ? "<span class='badge bg-secondary'>Sin stock</span>"
                                    :Convert.ToInt32(Eval("StockActual")) < Convert.ToInt32(Eval("StockMinimo")) 
                                     ?"<span class='badge bg-danger'>Bajo</span>" 
                                      : "<span class='badge bg-success'>OK</span>" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditarPage" runat="server"
                                        CssClass="btn btn-sm btn-outline-primary me-1"
                                        Text="Editar" CommandName="Editar"
                                        CommandArgument='<%# Eval("Id") %>' />
                                    <asp:Button ID="btnEliminarPage" runat="server"
                                        CssClass="btn btn-sm btn-outline-danger"
                                        Text="Eliminar" CommandName="Eliminar"
                                        CommandArgument='<%# Eval("Id") %>'
                                        OnClientClick="return confirm('¿Está seguro que desea eliminar este producto?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info">No hay productos para mostrar</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <!-- Mensaje de feedback -->
                <asp:Label ID="lblMensaje" runat="server" CssClass="mt-2" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
