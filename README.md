# SalesTaxes
This is a development candidate coding test for DealerOn
 
# Assumptions
1.  Whenever multiple of an item occur in a shopping basket, they will be of the same price
2.  A quantity, item name and price will always be provided.
3.  Assumes that sales tax and import tax are the same and can be lumped together in the "Sales Tax" total.
4.  Assume that only 1 shopping basket is entered at a time.
5.  Tax and import fee are constant and therefore are set at top of program.
6.  Cannot assume debug or release mode, so need to make sure data displays in either.

# Design Considerations
1.  To prevent errors in finding a specific item, all items are compared in CAPITAL letters.  
2.  Inventory Item is a separate struct from the main program to keep it isolated and is used to populate a list of inventory  at the beginning
3.  Shopping Basket Item is a separate class as well and is used as a collection or Shopping Basket
4.  User is able to continue to enter data until "Exit" in any capitalization is entered.
5.  To allow for data to be presented whether in debug or release mode, an additional read-line is added to output. To allow for exiting in debug, user must hit enter twice to allow for second read-line.