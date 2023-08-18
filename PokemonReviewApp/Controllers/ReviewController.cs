using AutoMapper;
using LayerDAL.Dto;
using LayerDAL.Interfaces;
using LayerDAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository repository, IMapper mapper)
        {   
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_repository.GetReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        public IActionResult GetReview(int reviewId) 
        {
            if (!_repository.ReviewExists(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_repository.GetReview(reviewId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(review);
        }

        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        public IActionResult GetReviewForAPokemon(int pokeId)
        {
            if (!_repository.ReviewExists(pokeId))
                return NotFound();

            var review = _mapper.Map<List<ReviewDto>>(_repository.GetReviewsOfAPokemon(pokeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

    }
}
