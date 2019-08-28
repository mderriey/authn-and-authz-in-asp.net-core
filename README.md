# Authentication and authorisation in ASP.NET Core

This is the repository used during a presentation at the [Alt.Net user group](https://www.meetup.com/en-AU/Sydney-Alt-Net/) in Sydney.

## Instructions

Each commit is supposed to show something different:

- After cloning the repo, you can navigate to the first commit by running `.\reset.ps1`
- Run the `.\next-commit.ps1` script to move to the next commit

### For authentication

1. What I consider a bad example seen in samples where authentication providers are not explicitly named and causes confusion for people not familar with the authN system in ASP.NET Core
1. An improvement over the past example where the provider is explicitly named
1. An example of how you would use a cookies and OIDC provider in an application (to delegate authentication to AAD, for example)
1. An example where you have both an MVC part (which serves HTML pages) and a Web API part and you want both to be always authenticated; the forwarding capability helps you do that
1. A case where you want to allow users to use social providers, show them on a page, and trigger authentication for the one the user picked
1. How to configure an authentication provider &mdash; with options and events
1. How to use DI when handling events &mdash; with a separate events class that is then registered in the DI container

### For authorisation

1. How to add the authZ services, add policies and use them with `[Authorize(Policy = "<policy-name>"]` attributes
1. An example of how to create a custom requirement and an associated handler that uses dependency injection
1. Show the concept of the default policy that is applied when you use an empty `[Authorize]` attribute
