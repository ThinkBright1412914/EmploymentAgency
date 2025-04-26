<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="HiringOrganizationList.aspx.cs" Inherits="EmploymentAgency.Admin.HiringOrganizationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-size: cover; background-repeat: no-repeat; background-position: center;">
        <div class="container-fluid pt-4 pb-4">
            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-success" ClientIDMode="Static" Visible="false"></asp:Label>
                </div>
            </div>
            <asp:HiddenField ID="hdnOrganizationId" runat="server" />
            <h3 class="text-center">Hiring Organization List/ Details</h3>

            <div class="tab-container text-center mb-4">
                <asp:LinkButton ID="btnNotApproved" runat="server"
                    CssClass="btn btn-outline-primary tab-button"
                    OnClick="btnNotApproved_Click">
                    Not Approved (<asp:Label ID="lblNotApprovedCount" runat="server" Text="0" />)
                </asp:LinkButton>

                <asp:LinkButton ID="btnApproved" runat="server"
                    CssClass="btn btn-outline-primary tab-button ml-3"
                    OnClick="btnApproved_Click">
                    Approved (<asp:Label ID="lblApprovedCount" runat="server" Text="0" />)
                </asp:LinkButton>
            </div>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered"
                        EmptyDataText="No Record to Display...!" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" DataKeyNames="OrganizationId">
                        <Columns>
                            <asp:BoundField DataField="S.No" HeaderText="S.No">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Organization Name">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PANNo" HeaderText="PAN No">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Position" HeaderText="Position">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NoOfEmployee" HeaderText="No of Employee">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="City" HeaderText="City">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PhoneNo" HeaderText="Phone No">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreateDate" HeaderText="Posted Date" DataFormatString="{0:dd MMMM yyyy}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Approve">
                              <ItemTemplate>
                                 <asp:LinkButton ID="btnApprove" runat="server" CommandName="Approve" CommandArgument='<%# Eval("OrganizationId") %>' CssClass="tick-icon" ToolTip="Approve">
                                   <i class="fa fa-check" style="font-size: 20px;"></i>
                                 </asp:LinkButton>
                              </ItemTemplate>         
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#7200cf" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function hideMessage() {
            var msg = document.getElementById("lblMsg");
            if (msg) {
                setTimeout(function () {
                    msg.style.display = "none";
                }, 3000);
            }
        }
    </script>
</asp:Content>



