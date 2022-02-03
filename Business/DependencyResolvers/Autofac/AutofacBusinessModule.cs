using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccsess.Abstract;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<BrandManager>().As<IBrandService>();
            builder.RegisterType<EfBrandDal>().As<IBrandDal>();

            builder.RegisterType<ClaimManager>().As<IClaimService>();
            builder.RegisterType<EfClaimDal>().As<IClaimDal>();

            builder.RegisterType<ColorManager>().As<IColorService>();
            builder.RegisterType<EfColorDal>().As<IColorDal>();

            builder.RegisterType<ContactManager>().As<IContactService>();
            builder.RegisterType<EfContactDal>().As<IContactDal>();

            builder.RegisterType<CreditCardManager>().As<ICreditCardSevice>();
            builder.RegisterType<EfCreditCardDal>().As<ICreditCardDal>();

            builder.RegisterType<ProductImageManager>().As<IProductImageService>();
            builder.RegisterType<EfProductImageDal>().As<IProductImageDal>();

            builder.RegisterType<ProfilePhotoManager>().As<IProfilePhotoService>();
            builder.RegisterType<EfProfilePhotoDal>().As<IProfilePhotoDal>();

            builder.RegisterType<RentalManager>().As<IRentalService>();
            builder.RegisterType<EfRentalDal>().As<IRentalDal>();

            builder.RegisterType<SaleManager>().As<ISaleService>();
            builder.RegisterType<EfSaleDal>().As<ISaleDal>();

            builder.RegisterType<BidManager>().As<IBidService>();
            builder.RegisterType<EfBidDal>().As<IBidDal>();

            builder.RegisterType<EvaluationManager>().As<IEvaluationService>();
            builder.RegisterType<EfEvaluationDal>().As<IEvaluationDal>();

            builder.RegisterType<BidImageManager>().As<IBidImageService>();
            builder.RegisterType<EfBidImageDal>().As<IBidImageDal>(); 
            
            builder.RegisterType<FavoriteManager>().As<IFavoriteService>();
            builder.RegisterType<EfFavoriteDal>().As<IFavoriteDal>();

            builder.RegisterType<TokenManager>().As<ITokenService>();
            builder.RegisterType<EfTokenDal>().As<ITokenDal>(); 
            
            builder.RegisterType<TokenOperationManager>().As<ITokenOperationService>();
            builder.RegisterType<EfTokenOperationDal>().As<ITokenOperationDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
