using System.Collections;
using System.Text;

namespace lab9
{
    internal class Program
    {
        public class Employee : IComparable, IComparable<Employee>, ICloneable
        {
            private string name;
            private string surname;
            private char gender;
            private int age;
            private int salary;

            public Employee(string n, string s, char g, int a, int b)
            {
                name = n;
                surname = s;
                gender = g;
                age = a;
                salary = b;
            }

            public int getAge()
            {
                return this.age;
            }

            public override string ToString()
            {
                return $"Name: {name}, Surname: {surname}, Gender: {gender}, Age: {age}, Salary: {salary}";
            }

            // Реалізація IComparable
            public int CompareTo(object obj)
            {
                if (obj is Employee other)
                    return this.age.CompareTo(other.age);
                else
                    throw new ArgumentException("Object is not an Employee");
            }

            // Реалізація IComparable<Employee>
            int IComparable<Employee>.CompareTo(Employee? other)
            {
                return this.age.CompareTo(other.age);
            }

            // Реалізація ICloneable
            public object Clone()
            {
                return new Employee(name, surname, gender, age, salary);
            }
        }



        static void Main(string[] args)
        {
            task3();
        }

        public static void task1()
        {
            string formula;
            string inputPath = "D:\\C#\\lab9\\data_task1.txt";
            

            using (FileStream readstream = File.OpenRead(inputPath))
            {
                byte[] array = new byte[readstream.Length];
                readstream.Read(array);
                formula = Encoding.Default.GetString(array);
            }

            Stack<int> values = new Stack<int>();
            Stack<char> operators = new Stack<char>();

            for (int i = 0; i < formula.Length; i++)
            {
                char c = formula[i];

                if (char.IsDigit(c))
                {
                    values.Push(c - '0');
                }
                else if (c == 'M' || c == 'm')
                {
                    operators.Push(c);
                    i++;
                }
                else if (c == ')')
                {
                    int b = values.Pop();
                    int a = values.Pop();
                    char op = operators.Pop();

                    int result = (op == 'M') ? Math.Max(a, b) : Math.Min(a, b);
                    values.Push(result);
                }
                
            }

            Console.WriteLine($"Результат: {values.Pop()}");
        }
        public static void task2()
        {
            List<Employee> employees = new List<Employee>();
            Queue<Employee> employees_under30 = new Queue<Employee>();
            Queue<Employee> employees_other = new Queue<Employee>();
            string inputPath = "D:\\C#\\lab9\\data_task2.txt";
            string employees_data;


            using (FileStream readstream = File.OpenRead(inputPath))
            {
                byte[] array = new byte[readstream.Length];
                readstream.Read(array);
                employees_data = Encoding.Default.GetString(array);
            }

            string[] emp_data = employees_data.Split("\n");
            foreach(String s in emp_data)
            {
                string[] empl = s.Split(",");
                employees.Add(strToEmp(empl));

                
            }

            foreach(Employee e in employees)
            {
                if (e.getAge() < 30)
                {
                    employees_under30.Enqueue(e);
                }
                else
                {
                    employees_other.Enqueue(e);
                }
            }


            Console.WriteLine("Працівники віком менше 30");
            foreach(Employee e in employees_under30)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Решта працівників");
            foreach (Employee e in employees_other)
            {
                Console.WriteLine(e);
            }


        }
        public static Employee strToEmp(string[]data)
        {
            data[0] = data[0].Replace("Name: ", "");
            data[1] = data[1].Replace(" Surname: ", "");
            data[2] = data[2].Replace(" Gender: ", "");
            data[3] = data[3].Replace(" Age: ", "");
            data[4] = data[4].Replace(" Salary: ", "");
            


            Employee result = new Employee(data[0], data[1], Char.Parse(data[2]), int.Parse(data[3]), int.Parse(data[4]));
            return result;
        }
        public static void task3()
        {
            // Завдання 1 
            string inputPath1 = "D:\\C#\\lab9\\data_task1.txt";
            string formula;

            using (FileStream fs = File.OpenRead(inputPath1))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                formula = Encoding.Default.GetString(buffer);
            }

            Stack<int> values = new Stack<int>();
            Stack<char> operators = new Stack<char>();

            for (int i = 0; i < formula.Length; i++)
            {
                char c = formula[i];

                if (char.IsDigit(c))
                {
                    values.Push(c - '0');
                }
                else if (c == 'M' || c == 'm')
                {
                    operators.Push(c);
                    i++; // пропустити дужку (
                }
                else if (c == ')')
                {
                    int b = values.Pop();
                    int a = values.Pop();
                    char op = operators.Pop();
                    int res = (op == 'M') ? Math.Max(a, b) : Math.Min(a, b);
                    values.Push(res);
                }
            }

            Console.WriteLine($"[task3] Результат формули: {values.Pop()}");

            // Завдання 2
            string inputPath2 = "D:\\C#\\lab9\\data_task2.txt";
            string employees_data;

            using (FileStream fs = File.OpenRead(inputPath2))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                employees_data = Encoding.Default.GetString(buffer);
            }

            string[] emp_data = employees_data.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            ArrayList allEmployees = new ArrayList();
            ArrayList employeesUnder30 = new ArrayList();
            ArrayList employeesOther = new ArrayList();

            foreach (string line in emp_data)
            {
                string[] parts = line.Split(",");
                Employee e = strToEmp(parts);
                allEmployees.Add(e);
            }

            foreach (Employee e in allEmployees)
            {
                Employee cloned = (Employee)e.Clone();
                if (cloned.getAge() < 30)
                    employeesUnder30.Add(cloned);
                else
                    employeesOther.Add(cloned);
            }

            allEmployees.Sort(); 
            Console.WriteLine("\nСписок усіх працівників (відсортовано за віком):");
            foreach (Employee e in allEmployees)
                Console.WriteLine(e);

            Console.WriteLine("\nПрацівники віком менше 30:");
            foreach (Employee e in employeesUnder30)
                Console.WriteLine(e);

            Console.WriteLine("\nРешта працівників:");
            foreach (Employee e in employeesOther)
                Console.WriteLine(e);
        
    }
            

    }
}


