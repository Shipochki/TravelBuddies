namespace TravelBuddies.Application.Review.Queries.GetReviewsByReciverId
{
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class GetReviewsByReciverIdHandler : BaseHandler, IRequestHandler<GetReviewsByReciverIdCommand, IEnumerable<Review>>
	{
		public GetReviewsByReciverIdHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<IEnumerable<Review>> Handle(GetReviewsByReciverIdCommand request, CancellationToken cancellationToken)
		{
			List<Review> reviews = await _repository
				.All<Review>(r => r.IsDeleted == false && r.ReciverId == request.Id)
				.ToListAsync();

			return await Task.FromResult(reviews);
		}
	}
}
