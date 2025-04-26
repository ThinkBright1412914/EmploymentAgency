<%@ Page Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Hiring.aspx.cs" Inherits="EmploymentAgency.User.Hiring" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfUserType" runat="server" />
  <section>
    <div class="container pt-50 pb-40">
        <div class="row">
            <div class="col-12">
                <h2 class="contact-title text-center">Create Hiring</h2>
            </div>

            <!-- Tabs -->
            <div class="col-12 mb-4 text-center">
                <ul class="nav nav-tabs justify-content-center" id="myTab" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="organization-tab" data-bs-toggle="tab" data-bs-target="#organization" type="button" role="tab" aria-controls="organization" aria-selected="true" onclick="showOrganization()">Organization</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="individual-tab" data-bs-toggle="tab" data-bs-target="#individual" type="button" role="tab" aria-controls="individual" aria-selected="false" onclick="showIndividual()">Individual</button>
                    </li>
                </ul>
            </div>

            <!-- Form Section -->
            <div class="col-lg-6 mx-auto">
                <div class="form-contact contact_form">
                    <div class="row">
                        <div class="col-12">
                            <div class="btn-toolbar justify-content-between mb-3">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="form-group">
                                <label>Organization / Individual Name</label>
                                <asp:TextBox ID="txtOrganizationName" runat="server" CssClass="form-control w-100" placeholder="Enter Name" required></asp:TextBox>
                            </div>
                        </div>

                        <!-- PAN No Section (We will hide/show this) -->
                        <div class="col-12" id="divPAN">
                            <div class="form-group">
                                <label>PAN No</label>
                                <asp:TextBox ID="txtPANNo" runat="server" CssClass="form-control w-100" placeholder="Enter PAN No"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <label>Position For</label>
                                <asp:TextBox ID="txtPositionFor" runat="server" CssClass="form-control w-100" placeholder="Enter Position" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <label>Number of Employees</label>
                                <asp:TextBox ID="txtNoOfEmployees" runat="server" CssClass="form-control w-100" placeholder="Enter Number Of Employees" TextMode="Number" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="form-group">
                                <label>Phone Number</label>
                                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control w-100" placeholder="Enter Phone Number" required></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Phone Number must have 10 digits!" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"
                                    Font-Size="Small" ValidationExpression="^[0-9]{10}$" ControlToValidate="txtPhoneNumber"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="form-group">
                                <label>Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control w-100" placeholder="Enter your Email" required TextMode="Email"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="form-group">
                                <label>City</label>
                                <asp:DropDownList ID="ddlCity" runat="server" DataSourceID="SqlDataSource1" CssClass="form-check w-100"
                                    AppendDataBoundItems="true" DataTextField="CityName" DataValueField="CityName">
                                    <asp:ListItem Value="0">Select City</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="City is required" ForeColor="Red" Display="Dynamic" Font-Size="Small" InitialValue="0" ControlToValidate="ddlCity"></asp:RequiredFieldValidator>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EmploymentAgencyConnectionString %>" ProviderName="<%$ ConnectionStrings:EmploymentAgencyConnectionString.ProviderName %>" SelectCommand="SELECT [CityName] FROM [City]"></asp:SqlDataSource>
                            </div>
                        </div>

                    </div>

                    <div class="form-group mt-3">
                        <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="button button-contactForm boxed-btn mr-4" OnClick="btnAdd_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</section>

    <style>
    .nav-tabs .nav-link {
        border: none;
        background-color: #f1f1f1;
        color: #333;
        padding: 10px 20px;
        margin: 0 5px;
        border-radius: 5px;
        transition: 0.3s ease;
    }

    .nav-tabs .nav-link.active {
        background-color: #007bff;
        color: white;
        font-weight: bold;
        box-shadow: 0 2px 6px rgba(0,0,0,0.1);
    }

    .nav-tabs .nav-link:hover {
        background-color: #e2e6ea;
    }
</style>

<script type="text/javascript">
    function showOrganization() {
        document.getElementById('divPAN').style.display = 'block';
        document.getElementById('<%= hfUserType.ClientID %>').value = 'Organization';

        document.getElementById('organization-tab').classList.add('active');
        document.getElementById('individual-tab').classList.remove('active');
    }

    function showIndividual() {
        document.getElementById('divPAN').style.display = 'none';
        document.getElementById('<%= hfUserType.ClientID %>').value = 'Individual';

        document.getElementById('individual-tab').classList.add('active');
        document.getElementById('organization-tab').classList.remove('active');
    }
</script>
</asp:Content>
