# Localhost API
A .NET Form app that runs on a Windows PC.  
It provides the framework to expose various local APIs to the web via a RestAPI hosted in the form app.

There are a few test local APIs incuded in the build (list installed printers, write a to test file, etc).


THERE IS NO SECURITY BUILT INTO THIS APP
So user beware.  The CORS configuration in the hosted WCF service are open ended so any domain can contact the
service from javascript in a webpage.
