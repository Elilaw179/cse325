using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
public PizzaController()
{
}

[HttpGet]
public ActionResult<List<Pizza>> GetAll() =>
    PizzaService.GetAll();

[HttpGet("{id}")]
public ActionResult<Pizza> Get(int id)
{
    var pizza = PizzaService.Get(id);

    if (pizza == null)
        return NotFound();

    return pizza;
}

// POST action
[HttpPost]
public IActionResult Create(Pizza pizza)
{
    PizzaService.Add(pizza);

    return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
}

// PUT action
[HttpPut("{id}")]
public IActionResult Update(int id, Pizza pizza)
{
    var existingPizza = PizzaService.Get(id);

    if (existingPizza == null)
        return NotFound();

    pizza.Id = id;

    PizzaService.Update(pizza);

    return NoContent();
}

// DELETE action
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    var pizza = PizzaService.Get(id);

    if (pizza == null)
        return NotFound();

    PizzaService.Delete(id);

    return NoContent();
}

}