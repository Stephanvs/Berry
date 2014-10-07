Berry
=====

Berry is a framework that allows you to create mobile apps with very little effort and provides you with both an architectural framework and lots of reusable code for your mobile apps on the Xamarin platform.

Mission
=======

Make the life of cross platform mobile development as easy and awesome as humanly possible while also greatly increasing the shared code pie slice across mobile platforms but also for projects in general. Building on existing and proven patterns and practices we want to enable you to write high quality and testable code while leveraging your existing skillset as a .NET developer.

Approach
========

Xamarin currently provides the Xamarin.Mobile (http://xamarin.com/mobileapi) API's as an abstraction over several platform specific services, such as contacts, GPS, the compass, the accelerometer, the notification service or the system calendar.

Part of Berry's approach is being an addition to the abstractions provided by the afore mentioned Xamarin.Mobile library. Abstracting these services is one step to reduce platform specific code. 

Additionally we also want to use Inversion of Control and Model View ViewModel techniques to increase code share. Therefore Berry provides Dependency Injection capabilities abstracted across platforms, as well as ViewModel bindings.

Why the name Berry?
===================

No particular reason, just if you think about an actual berry (you know the fruit), they consist of small components. This is exactly what this library is all about.
