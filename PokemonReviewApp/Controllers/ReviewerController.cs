﻿using AutoMapper;
using LayerDAL.Dto;
using LayerDAL.Interfaces;
using LayerDAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _repository;
        private readonly IMapper _mapper;
        public ReviewerController(IReviewerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_repository.GetReviewers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        public IActionResult GetReview(int reviewerId)
        {
            if (!_repository.ReviewerExists(reviewerId))
                return NotFound();

            var reviewer = _mapper.Map<ReviewerDto>(_repository.GetReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}/reviewes")]
        [ProducesResponseType(200, Type = typeof(Review))]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_repository.ReviewerExists(reviewerId))
                return NotFound();

            var reviewes = _mapper.Map<List<Review>>(_repository.GetReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewes);
        }
    }
}
