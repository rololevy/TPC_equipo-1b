<%@ Page Title="Gestión de Stock" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="Equipo1b_TPC.Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Stock</h2>

        <div class="row mb-3">
            <div class="col-md-4">
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar producto..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="-- Todas las marcas --"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="-- Todas las categorías --"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary w-100" OnClick="btnLimpiar_Click" />
            </div>
        </div>

        <div class="row">
            <div class="col-12">
                <asp:GridView ID="dgvStock" runat="server" CssClass="table table-striped table-bordered" 
                    AutoGenerateColumns="false" DataKeyNames="Id" OnRowCommand="dgvStock_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="Id" />
                        <asp:BoundField HeaderText="Producto" DataField="Nombre" />
                        <asp:BoundField HeaderText="Marca" DataField="Marca.Nombre" />
                        <asp:BoundField HeaderText="Categoría" DataField="Categoria.Nombre" />
                        <asp:BoundField HeaderText="Stock Actual" DataField="StockActual" ItemStyle-CssClass="text-center" />
                        <asp:BoundField HeaderText="Stock Mínimo" DataField="StockMinimo" ItemStyle-CssClass="text-center" />
                        <asp:BoundField HeaderText="Precio" DataField="PrecioCompra" DataFormatString="{0:C2}" />
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# Convert.ToInt32(Eval("StockActual")) < Convert.ToInt32(Eval("StockMinimo")) 
                                    ? "<span class='badge bg-danger'>Stock Bajo</span>" 
                                    : "<span class='badge bg-success'>OK</span>" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnAjustar" runat="server" CssClass="btn btn-sm btn-primary" 
                                    CommandName="Ajustar" CommandArgument='<%# Eval("Id") %>'>
                                    Ajustar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <asp:Panel ID="pnlModal" runat="server" CssClass="modal-overlay" Visible="false">
            <div class="modal-content-custom">
                <h4>Ajustar Stock</h4>
                <asp:HiddenField ID="hfIdProducto" runat="server" Value="0" />
                <div class="mb-3">
                    <asp:Label ID="lblProducto" runat="server" Text="" CssClass="form-label fw-bold"></asp:Label>
                </div>
                <div class="mb-3">
                    <label class="form-label">Nuevo Stock:</label>
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
                <div class="d-flex justify-content-end">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary me-2" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>

    <style>
        .modal-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0,0,0,0.5);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 1000;
        }
        .modal-content-custom {
            background: white;
            padding: 30px;
            border-radius: 10px;
            min-width: 400px;
        }
    </style>
</asp:Content>
