# Train and Test: Dependency Injection and Request Pipelines

I used here `MediatR` and `autofac`. The examples are pretty contrived stuff, 
it's mostly only about using/applying the two libs.

MediatR can be used with any DI/IoC framework. Because I wanted to try it 
(never used it yet in any project), I test autofac here. Interesting in
MediatR are the possibilities to add behavior to the request pipeline.

This could allow to handle orthogonal system aspects like logging, permissions,
audit trailing, performance surveillance... etc. in an elegant way, iff your
application works via CQRS or more generally via a request handling interface.

Recently I really think that request based interfaces/architecture can be very 
strong. When requests are your interface between domain layer and outside 
layers you are pretty modular. You could see each request as a kind of nano-service.  

Also I like the idea of requests/commands. They define an interface by more via
data than via functions. I feel that this is often the better way: you can evade
dependencies on specific functions (function name and namespace + 
return type and input type) and reduce to a dependency on return and input type
only.

Still: This architecture pattern probably has limitations and needs a lot of
setup (which makes tools like mediatR helpful). Thought this is probably true,
I am not yet sure what the weaknesses (of the pattern not mediatR) are.