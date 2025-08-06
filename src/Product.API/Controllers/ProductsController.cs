using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Commands.CreateProduct;
using Product.Application.Commands.UpdateProduct;
using Product.Application.Queries.GetAllProducts;
using Product.Application.Queries.GetProductById;
using Product.Application.DTOs;

namespace Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all products (pour afficher la liste dans le front)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] bool? onlyActive = true)
    {
        var query = new GetAllProductsQuery { OnlyActive = onlyActive };
        var products = await _mediator.Send(query);
        return Ok(products);
    }

    /// <summary>
    /// Get product by ID (pour les détails)
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var query = new GetProductByIdQuery(id);
        var product = await _mediator.Send(query);
        
        if (product == null)
            return NotFound();
            
        return Ok(product);
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateProductCommand command)
    {
        try
        {
            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
        }
        catch (Application.Exceptions.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    /// <summary>
    /// Updates an existing product
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Application.Exceptions.NotFoundException)
        {
            return NotFound();
        }
        catch (Application.Exceptions.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    // I'll implement these handlers myself for better understanding of CQRS pattern:
    // - DeleteProductCommand 
    // - GetProductsByCategoryQuery
}