<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Lmbtfy.Web.Models.GeneratedUrlModel>" %>

<div class="center">
    <h1>Share the link below!</h1>
    <input id="lmbtfyLink" value="<%= Html.Encode(Model.TinyUrl)%>" />
    
    <p>
        Alternatively, provide the direct link: <strong><a href="<%= Html.Encode(Model.Url)%>" title="The Url"><%= Html.Encode(Model.Url)%></a></strong><br />
        The URL <a href="<%=Html.Encode(Model.TinyUrl)%>" title="Tiny URL"><%= Html.Encode(Model.TinyUrl)%></a> was shortened using <a href="http://tinyurl.com" title="Tiny Url">TinyUrl.com</a>.
    </p>
</div>