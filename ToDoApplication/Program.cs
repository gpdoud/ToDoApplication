using System;
using System.Threading.Tasks;

using ToDoLib.Controllers;
using ToDoLib.Models;

namespace ToDoApplication {

    class Program {

        TodosController todoCtrl = null;
        CategoriesController catCtrl = null;

        async Task ListAllTodos() {
            Cli.DisplayLine("Called ListAllTodos()");
            Cli.DisplayLine();
            var todos = await todoCtrl.GetAll();
            Console.WriteLine($"{Todo.Header()}");
            foreach(var todo in todos) {
                Console.WriteLine($"{todo}");
            }
            Cli.DisplayLine();
            Cli.DisplayLine("Done ...");
            Cli.DisplayLine();
        }
        async Task CreateTodo() {
            Cli.DisplayLine("Called CreateTodo()");
            Cli.DisplayLine();
            var categories = await catCtrl.GetAll();
            var todo = new Todo();
            todo.Id = 0;
            todo.Description = Cli.GetString("Enter description");
            todo.Due = Cli.GetDateTime("Enter due date");
            todo.Note = Cli.GetString("Enter note");
            Cli.DisplayLine("Categories:");
            foreach(var c in categories) {
                Cli.DisplayLine($" {c.Id} : {c.Name}");
            }
            
            todo.CategoryId = Cli.GetInt("Select category");
            var newTodo = await todoCtrl.Create(todo);
            Cli.DisplayLine();
            Cli.DisplayLine("Added ...");
            Cli.DisplayLine();
        }
        async Task UpdateTodo() {
            Cli.DisplayLine();
            Cli.DisplayLine("List of todo items");
            Cli.DisplayLine();
            var todos = await todoCtrl.GetAll();
            Console.WriteLine($"{Todo.Header()}");
            foreach(var todo in todos) {
                Console.WriteLine($"{todo}");
            }
            Cli.DisplayLine();
            var id = Cli.GetInt("Enter todo id");
            Cli.DisplayLine();
            var todo1 = await todoCtrl.GetByPK(id);
            var chgDescription = Cli.GetBoolean("Change description?");
            if(chgDescription) {
                //Cli.DisplayLine($"Description value is {todo1.descr}");
                todo1.Description = Cli.GetString($"");
            }
        }
        async Task Run() {
            todoCtrl = new TodosController();
            catCtrl = new CategoriesController();
            Cli.DisplayLine("Todo Application");
            var option = DisplayMenu();
            while(option != 0) {
                switch(option) {
                    case 1:
                        await ListAllTodos();
                        break;
                    case 2:
                        await CreateTodo();
                        break;
                    case 3:
                        await UpdateTodo();
                        break;
                    case 0:
                        return;
                    default:
                        Cli.DisplayLine("Invalid menu option");
                        break;
                }
                option = DisplayMenu();
            }
        }
        int DisplayMenu() {
            Cli.DisplayLine("Menu:");
            Cli.DisplayLine(" 1 : List all todos");
            Cli.DisplayLine(" 2 : Add todo");
            Cli.DisplayLine(" 3 : Update todo");
            Cli.DisplayLine(" 0 : Exit");
            var option = Cli.GetInt("Enter menu number");
            Cli.DisplayLine();
            return option;
        }

        static async Task Main(string[] args) {
            var pgm = new Program();
            await pgm.Run();
        }
    }
}
