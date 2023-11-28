
using System.Reflection;

Assembly asm = Assembly.LoadFrom("LibraryClass.dll");

//Type t = asm.GetType("Class1"); почему не работает? пишет не возвращено значений

Type[] types = asm.GetTypes();
Type type = asm.GetType(types[0].ToString());

MethodInfo mi = type.GetMethod("test", BindingFlags.NonPublic | BindingFlags.Static);
object result = mi.Invoke(null, new object[] { 2 });
Console.WriteLine(result); 

