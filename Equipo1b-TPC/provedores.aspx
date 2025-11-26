<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="provedores.aspx.cs" Inherits="Equipo1b_TPC.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Contenedor principal */
        .card-compras {
            background: linear-gradient(135deg, #0d6efd, #5fa8ff);
            border-radius: 15px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.2);
        }
        
        /* Contenedor del grid */
        .grid-wrapper {
            background-color: white;
            border-radius: 8px;
            max-height: 320px;
            overflow-y: auto;
            border: 2px solid #0d6efd;
            padding: 0;
        }
        
        /* Estilos generales para el GridView */
        .grid-wrapper table {
            width: 100%;
            border-collapse: collapse;
            margin: 0;
        }
        
        .grid-wrapper table th {
            position: sticky;
            top: 0;
            z-index: 10;
        }
        
        /* Caja del Total */
        .total-box {
            background-color: rgba(255, 255, 255, 0.25);
            border: 3px solid #fff;
            border-radius: 12px;
            padding: 15px 25px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
        }
        
        .total-amount {
            font-size: 1.8rem;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.4);
            font-weight: bold;
        }
        
        /* Mensajes */
        .mensaje-box {
            padding: 10px 15px;
            border-radius: 8px;
            font-weight: bold;
            text-align: center;
            margin-bottom: 15px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
        }
        
        .mensaje-success {
            background-color: #28a745;
            color: white;
            border: 2px solid #1e7e34;
        }
        
        .mensaje-danger {
            background-color: #dc3545;
            color: white;
            border: 2px solid #bd2130;
        }
        
        .mensaje-warning {
            background-color: #ffc107;
            color: #000;
            border: 2px solid #d39e00;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upProvedores" runat="server">
        <ContentTemplate>
            <div class="container-fluid mt-4 px-3">
                <div class="row justify-content-center">
                    <div class="col-12 col-xl-11">
                        <div class="card-compras p-4 text-white">
                            <h2 class="text-center mb-4 fw-bold">Registro de Compras</h2>
                            
                            <!-- Selección de Proveedor -->
                            <div class="row mb-3 align-items-end">
                                <div class="col-md-2">
                                    <label class="text-white fw-bold mb-1">Proveedor:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtFiltrarProvedores" AutoPostBack="true" OnTextChanged="txtFiltrarProvedores_TextChanged" placeHolder="Buscar por Razón Social" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlProvedores" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProvedores_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>
                            </div>

                            <!-- Mensaje -->
                            <div class="row mb-3">
                                <div class="col-12">
                                    <asp:Label ID="lblMensaje" Visible="false" CssClass="mensaje-box" runat="server" Text=""></asp:Label>
                                </div>
                            </div>

                            <!-- Campos de Ingreso de Producto -->
                            <div class="row mb-3 align-items-end">
                                <div class="col-md-4">
                                    <label class="text-white fw-bold mb-1">Producto:</label>
                                    <asp:DropDownList ID="ddlProductos" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label class="text-white fw-bold mb-1">Cantidad:</label>
                                    <asp:TextBox ID="txtCantidad" CssClass="form-control" placeHolder="0" TextMode="Number" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" 
                                        ControlToValidate="txtCantidad" 
                                        ErrorMessage="Requerido" 
                                        Display="Dynamic"
                                        ForeColor="Yellow" 
                                        ValidationGroup="AgregarProducto" />
                                </div>
                                <div class="col-md-2">
                                    <label class="text-white fw-bold mb-1">Precio:</label>
                                    <asp:TextBox ID="txtPrecioUnitario" CssClass="form-control" placeHolder="0.00" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" 
                                        ControlToValidate="txtPrecioUnitario" 
                                        ErrorMessage="Requerido" 
                                        Display="Dynamic"
                                        ForeColor="Yellow" 
                                        ValidationGroup="AgregarProducto" />
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chkFiltros" AutoPostBack="true" CssClass="form-check-label text-white fw-bold" Text="Filtros Avanzados" OnCheckedChanged="chkFiltros_CheckedChanged" runat="server" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnAgregarProducto" CssClass="btn btn-light fw-bold w-100" runat="server" Text="➕ Agregar" OnClick="btnAgregarProducto_Click" ValidationGroup="AgregarProducto" />
                                </div>
                            </div>

                            <!-- Filtros Avanzados -->
                            <%if (filtroAvanzado) { %>
                            <div class="row mb-3 align-items-end">
                                <div class="col-md-5">
                                    <label class="text-white fw-bold mb-1">Marca:</label>
                                    <asp:DropDownList ID="ddlMarcas" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltros_Changed" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-5">
                                    <label class="text-white fw-bold mb-1">Categoría:</label>
                                    <asp:DropDownList ID="ddlCategorias" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltros_Changed" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnLimpiarFiltros" CssClass="btn btn-secondary w-100" runat="server" Text="🔄 Limpiar" OnClick="btnLimpiarFiltros_Click" />
                                </div>
                            </div>
                            <% } %>

                            <!-- Botones de Acción Principal -->
                            <div class="row mb-4">
                                <div class="col-12 text-center">
                                    <asp:Button ID="btnRegistrarCompra" CssClass="btn btn-success btn-lg me-3 px-5" runat="server" Text="💾 Registrar Compra" OnClick="btnRegistrarCompra_Click" CausesValidation="false" />
                                    <asp:Button ID="btnCancelar" CssClass="btn btn-danger btn-lg px-5" runat="server" Text="❌ Cancelar" OnClick="btnCancelar_Click" CausesValidation="false" />
                                </div>
                            </div>

                            <hr class="text-white mb-3" />

                            <!-- GridView de Productos -->
                            <div class="row mb-3">
                                <div class="col-12">
                                    <h5 class="text-white fw-bold mb-2">📋 Productos en la Compra</h5>
                                    <div class="grid-wrapper">
                                        <asp:GridView ID="gvProductos" runat="server" 
                                            AutoGenerateColumns="false" 
                                            ShowHeaderWhenEmpty="true"
                                            EmptyDataText="No hay productos agregados. Seleccione un proveedor y agregue productos."
                                            GridLines="Both"
                                            CellPadding="8"
                                            CellSpacing="0"
                                            OnRowCommand="gvProductos_RowCommand">
                                            <HeaderStyle BackColor="#0d6efd" ForeColor="White" Font-Bold="true" Height="40px" />
                                            <RowStyle BackColor="White" ForeColor="Black" Height="35px" />
                                            <AlternatingRowStyle BackColor="#f8f9fa" ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundField DataField="ProductoId" HeaderText="ID">
                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NombreProducto" HeaderText="Producto">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Cantidad" HeaderText="Cant.">
                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unit." DataFormatString="{0:C2}">
                                                    <ItemStyle Width="130px" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="130px" HorizontalAlign="Right" Font-Bold="true" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Acción">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEliminar" runat="server" 
                                                            CommandName="Eliminar" 
                                                            CommandArgument='<%# Eval("ProductoId") %>' 
                                                            CssClass="btn btn-danger btn-sm"
                                                            CausesValidation="false"
                                                            OnClientClick="return confirm('¿Eliminar este producto del detalle?');">
                                                            🗑️
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <!-- Total Destacado -->
                            <div class="row mt-3">
                                <div class="col-12 d-flex justify-content-end">
                                    <div class="total-box">
                                        <span class="text-white me-3" style="font-size: 1.3rem;">TOTAL:</span>
                                        <asp:Label ID="lblTotal" runat="server" Text="$0.00" CssClass="total-amount text-white"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
