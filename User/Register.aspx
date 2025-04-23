<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EmploymentAgency.User.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <div class=" container pt-50 pb-40">
            <div class="row">
                <div class="col-12 pb-20">
                    <h6>Personal Information</h6>
                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="col-12">
                    <h2 class="contact-title text-center">Sign Up</h2>
                </div>
                <div class="col-lg-6 mx-auto">
                    <div class="form-contact contact_form">
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control w-100" placeholder="Enter Unique Username" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label>Password</label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control w-100" placeholder="Enter Password"
                                        TextMode="Password" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label>Confirm Password</label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control w-100" placeholder="Enter Confirm Password" TextMode="Password" required></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password & Confirm Password Should Be Same."
                                        ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"
                                        Font-Size="Small"></asp:CompareValidator>
                                </div>
                            </div>

                            <div class="col-12">
                                <h6>Personal Information</h6>
                            </div>

                            <div class="col-12">
                                <div class="form-group">
                                    <label>Full Name</label>
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control w-100" placeholder="Enter Full Name" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Name must be in characters" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"
                                        Font-Size="Small" ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txtFullName"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="form-group">
                                    <label>Address</label>
                                    <asp:TextBox ID="txtAddesss" runat="server" CssClass="form-control w-100" placeholder="Enter Address" TextMode="MultiLine" required></asp:TextBox>
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
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control w-100" placeholder="Enter Full Name" required TextMode="Email"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="form-group">
                                    <label>City</label>
                                    <asp:DropDownList ID="ddlCity" runat="server" DataSourceID="SqlDataSource1" CssClass=" form-check w-100"
                                        AppendDataBoundItems="true" DataTextField="CityName" DataValueField="CityName">
                                        <asp:ListItem Value="0">Select City</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="City is required" ForeColor="Red" Display="Dynamic" Font-Size="Small" InitialValue="0" ControlToValidate ="ddlCity"></asp:RequiredFieldValidator>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EmploymentAgencyConnectionString %>" ProviderName="<%$ ConnectionStrings:EmploymentAgencyConnectionString.ProviderName %>" SelectCommand="SELECT [CityName] FROM [City]"></asp:SqlDataSource>
                                </div>
                            </div>

                        </div>

                        <div class="form-group mt-3">
                            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="button button-contactForm boxed-btn mr-4" OnClick="btnRegister_Click" />
                            <span class ="clickLink"><a href ="../User/Login.aspx">Already Register? Click Here...</a></span>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
