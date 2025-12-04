<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="DetalleVentas.aspx.cs" Inherits="Equipo1b_TPC.HistorialVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="card-grid p-4  text-white" style="min-width: 400px; background: linear-gradient(135deg, #0d6efd, #5fa8ff); border-radius: 15px;">
            <asp:UpdatePanel ID="UpDetalleVentas" runat="server">
                <ContentTemplate>
                    <div class="col-12 mt-4">
                        <h4 class="text-white text-center">Historial general de ventas</h4>
                        <asp:GridView ID="gvDetalleVentas" ClientIDMode="static" ShowHeaderWhenEmpty="true" EmptyDataText="No hay ventas que mostrar" CssClass="table table-striped table-bordered text-center w-100" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:BoundField DataField="NumeroFactura" HeaderText="Numero de factura" />
                                <asp:BoundField DataField="TipoFactura" HeaderText="Tipo de factura" />
                                <asp:BoundField DataField="FechaVenta" HeaderText="Fecha de venta" />
                                <asp:BoundField DataField="Cliente.Id" HeaderText="Cliente id" />
                                <asp:BoundField DataField="TotalVenta" HeaderText="Total venta" />
                                <asp:BoundField DataField="MedioPago" HeaderText="Medio de pago" />
                                <asp:BoundField DataField="NroCierreCaja" Visible="false" />
                                <asp:TemplateField HeaderText="Detalle">
                                    <ItemTemplate>
                                        <asp:Button ID="btnVerDetalle" runat="server" CssClass="btn btn-sm btn-outline-primary mr-1" OnClick="btnVerDetalle_Click" Text="Ver detalle" CommandArgument='<%# Eval("NumeroFactura") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="d-flex justify-content-center align-items-center">
                            <asp:Label ID="lblMensaje" CssClass="text-danger fw-bold text-center" Visible="false" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
</asp:Content>
