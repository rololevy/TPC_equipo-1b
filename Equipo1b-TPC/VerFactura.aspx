<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="VerFactura.aspx.cs" Inherits="Equipo1b_TPC.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/Content/Impresion.css" media="print" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="printArea" class="container mt-4">
        <asp:UpdatePanel ID="UpItemsVenta" runat="server">
            <ContentTemplate>
                <div class="col-12 mt-4 mb-4">
                    <div class="card-grid p-4  text-white" style="min-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
                        <div class="text-center mb-3">
                            <asp:Label ID="lblDetalleFactura" CssClass="h4 text-white text-center" Text="Detalle Factura" runat="server"></asp:Label>
                        </div>
                        <div class="row mt-3 text-center">
                            <div class="col-2">
                                <asp:Label ID="lblNumeroFactura" runat="server" CssClass="text-center form-label fw-bold" Text="Numero de Factura"></asp:Label>
                                <asp:TextBox ID="txtNumeroFactura" ReadOnly="true" CssClass="form-control text-center" ClientIDMode="Static" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-2">
                                <asp:Label ID="lblTipoFactura" runat="server" CssClass="text-center form-label fw-bold" Text="Tipo de factura"></asp:Label>
                                <asp:TextBox ID="txtTipoFactura" ReadOnly="true" CssClass="form-control text-center" ClientIDMode="Static" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="LblNCliente" runat="server" CssClass="text-center form-label fw-bold" Text="Nombre del cliente"></asp:Label>
                                <asp:TextBox ID="TxtNombreCliente" ReadOnly="true" CssClass="form-control text-center" ClientIDMode="Static" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-2">
                                <asp:Label ID="LblMedioPago" runat="server" CssClass="text-center form-label fw-bold" Text="Medio de pago"></asp:Label>
                                <asp:TextBox ID="txtMedioPago" ReadOnly="true" CssClass="form-control text-center" ClientIDMode="Static" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="lblFecha" runat="server" CssClass="text-center form-label fw-bold" Text="Fecha De la factura"></asp:Label>
                                <asp:TextBox ID="txtFecha" ReadOnly="true" CssClass="form-control text-center fecha-factura" ClientIDMode="Static" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="mt-4">
                            <asp:GridView ID="gvItemsVentas" ClientIDMode="static" ShowHeaderWhenEmpty="true" EmptyDataText="No hay ventas que mostrar" CssClass="table table-striped table-bordered text-center w-100" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="id" Visible="false" />
                                    <asp:BoundField DataField="Producto.Nombre" HeaderText="Nombre del articulo" />
                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="PrecioUnitario" />
                                    <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" />
                                </Columns>
                            </asp:GridView>
                            <div class="d-flex justify-content-center align-items-center">
                                <asp:Label ID="lblMensaje" CssClass="text-danger fw-bold text-center" Visible="false" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <div class="row mt-1">
                            <div class="col-12 d-flex justify-content-end">
                                <div style="width: auto;" class="text-center">
                                    <asp:Label ID="lblTotalFactura" runat="server" Visible="false" CssClass="text-center form-label fw-bold w-100" Text="Total"></asp:Label>
                                    <asp:TextBox ID="txtTotal" ReadOnly="true" Visible="false" CssClass="text-center form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
        <asp:Button ID="btnImprimir" runat="server" Text="ImprimirFactura" CssClass="btn btn-primary mt-3" OnClientClick="imprimirFactura(); return false;" />
    </div>
    <script type="text/javascript">
        function imprimirFactura() {
            window.print();
        }
    </script>
</asp:Content>
