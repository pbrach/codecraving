# Command Unit Model - An Idea

The following concept is something pretty similar to the actor or agent model but perhaps extended to some degree in some aspects.

This model is intended for all kinds of domains: from games, simple problem solving, control and simulation produciton plants to business applications. Here however I try to embedd the whole thing into a industry plan-simulation like environment.


## Detailed Description of Units and Macros

1. Units have (basic declaration):
    - different types of units with different capabilities exist
    - an inner state
    - can process commands (send by a client)
    - work always async (they live in their own thread)
    - they have some outer state (which corresponds to the domain in which they act)
    - processing commands leads to changes in inner and outer state

2. Interaction:
    - clients can interact via commands directly with the unit (client -> unit communication)
    - unit processes the command and changes its own and the outer state
    - the client is informed about the results of his command by the state change of the inner unit state. the inner unit state can always be inspected. (unit -> client communication)

3. Outer State ?:
    - is not clear yet how exactly we want to work with it
    - could be a cross interaction field: influenced by client but definitely by the unit
    - also not clear: how is the outer state injected into the unit?
    - are there scoping rules for the outer state?

4. Macros:
    - are a composition of several units (units are like 'micro states', their aggregation like a 'macro state')
    - at least two till infinite units may form a macro
    - the macro has the accumulated trais and state of its units
    - units are not freely composable: it is only possible if they provide an aggregation interface for aggregating other units
    - a macro delegates commands to its units
    - not clear: may units within a macro accept individual commands, or should that be disabled within an aggregation?

5. Aggregation Contract of Units: 
    - ensures that only sane aggregations/macros can be composed (each macro is basically a functional unit and thus should provide a sane/reasonable functionality)
    - more concrete it:
    - specifies which types of units may aggreagte
    - specifies how many units may aggregate to this unit
    - can specify a 'cooperation-extension-capability' which is one or more new operations that are exposed only if two or more (identical or different) units are composed
    - a default 'cooperation-extension-capability' that needs to be always provided is the strategy how message handling between the composed units is handled, so that clients can interact with the macro in the exact same way as with a unit. the only difference for the user is that the amount of processable commands and state information is larger
    - consequently the composition of macros with other macros is possible
    - normally the composition of two units gives a macro. further composition (to a 3-units or larger macro) is done via composing a single unit with the 2-unit macro or via composition with another macro

