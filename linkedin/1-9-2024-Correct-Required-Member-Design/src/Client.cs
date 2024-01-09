public class Client
{
    public void Test1()
    {
        // If the 'Name' and 'Surname' properties are required, creating a user instance
        // without filling these properties must be prevented, but we can.
        var worstUser = new WorstUserDesign();
        // *****************************************
        // Still bad. Because, I can still modify the 'Name' and 'Surname' properties with null or other values.
        // Because these properties have public setters.
        // Also, it is possible to initialize a user with null values for required attributes.
        var badUser = new BadUserDesign("John", "Doe"); // (name: "null", surname: "null");
        badUser.Name = null; // = "Mary";
        badUser.Surname = null; // = "Smith";
    }

    public void Test2()
    {
        // Good. Because, I can't modify the 'Name' and 'Surname' properties with null or other values.
        // Because these properties have private setters.
        // Also, passing any null value to any required arguments throws an exception.
        var goodUser = new GoodUserDesign("John", "Doe"); // (name: "null", surname: "null");
        goodUser.Name = null; // = "Mary";
        goodUser.Surname = null; // = "Smith";
        // *****************************************
        // Best. Because, now the IDE warns clients about invalid inputs and redundant null checks.
        var bestUser1 = new BestUserDesign(null, null);

        // Case1: Rider & R# message: 'Possible 'null' assignment to non-nullable entity'.
        var bestUser2 = new BestUserDesign("John", "Doe");

        // Case 2: Rider & R# message: 'Expression is always true' message.
        if (bestUser2.Name != null)
        {
            var test = "Foo";
        }
    }
}