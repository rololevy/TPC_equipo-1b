<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="Equipo1b_TPC.FormularioProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0">
                    <asp:Label ID="lblTitulo" runat="server" Text="Nuevo Producto"></asp:Label>
                </h4>
            </div>
            <div class="card-body">
                <div class="row">
                    <!-- Columna izquierda -->
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label class="form-label">Nombre *</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre del producto"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Descripción</label>
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" 
                                TextMode="MultiLine" Rows="3" placeholder="Descripción del producto"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Marca</label>
                            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Categoría</label>
                            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Proveedor</label>
                            <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                    </div>

                    <!-- Columna derecha -->
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label class="form-label">Precio de Compra *</label>
                            <asp:TextBox ID="txtPrecioCompra" runat="server" CssClass="form-control" 
                            step="0.01" placeholder="0.00"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPrecioCompra" ErrorMessage="ingrese un precio valido" ValidationExpression="^\d+(\,\d{1,2}|\.\d{1,2})?$"  CssClass="text-danger fw-bold" ></asp:RegularExpressionValidator>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Porcentaje de Ganancia (%) *</label>
                            <asp:TextBox ID="txtPorcentajeGanancia" runat="server" CssClass="form-control" 
                                TextMode="Number" placeholder="0"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Stock Actual *</label>
                            <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" 
                                TextMode="Number" placeholder="0"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Stock Mínimo *</label>
                            <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" 
                                TextMode="Number" placeholder="0"></asp:TextBox>
                        </div>

                        <div class="mb-3 form-check">
                            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input" Checked="true" />
                            <label class="form-check-label">Activo</label>
                        </div>
                    </div>
                </div>

                <!-- Botones -->
                <div class="d-flex justify-content-between mt-3">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                        CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                        CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                </div>

                <!-- Mensaje -->
                <asp:Label ID="lblMensaje" runat="server" CssClass="mt-3" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
