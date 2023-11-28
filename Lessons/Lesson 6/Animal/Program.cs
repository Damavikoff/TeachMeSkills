using AnimalLibrary;

var cat = new Cat { Age = 3, Colour = "grey", Name = "Syuzan" };
var dog = new Dog { Age = 5, Colour = "brown", Name = "Jacob" };
var cow = new Cow { Age = 6, Colour = "black and white", Name = "Michel" };
var bird = new Bird { Age = 2, Colour = "red", Name = "Peep" };
var kitten = new Kitten { Age = 1, Colour = "orange", Name = "Mike" };

kitten.MakeSound();
kitten.MakeAction(cat);

dog.MakeSound();
cow.MakeSound();
cat.MakeSound();
bird.MakeSound();

cat.MakeAction(dog);
cow.MakeAction(bird);
bird.MakeAction(cat);
dog.MakeAction(cow);

cow.Print();
bird.Print();
kitten.Print();

cat.Print();
cat.AddAge(1);
cat.Print();
kitten.Print();
kitten.AddAge(2);
kitten.Print();
cat.Print();