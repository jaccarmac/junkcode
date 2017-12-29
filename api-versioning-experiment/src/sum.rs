extern crate rocket;
extern crate rocket_contrib;

use versioning;

#[get("/sum?<nums>")]
fn sum(nums: SumParams) -> versioning::VersionedResourceWrapper<SumResource> {
    versioning::VersionedResourceWrapper::new(SumResource {
        sum: nums.x + nums.y,
    })
}

#[derive(FromForm)]
struct SumParams {
    x: i32,
    y: i32,
}

#[derive(Serialize)]
struct SumResource {
    sum: i32,
}

impl<'r> rocket::response::Responder<'r> for SumResource {
    fn respond_to(
        self,
        request: &rocket::Request,
    ) -> Result<rocket::Response<'r>, rocket::http::Status> {
        rocket_contrib::Json(self).respond_to(request)
    }
}

impl<'r> versioning::VersionedResource<'r> for SumResource {
    type SumResourceWithXY = SumResourceWithXY;
    type NotApplicable = SumResourceWithXY;
    type SumResourceOnlyString = SumResourceOnlyString;
}

#[derive(Serialize)]
struct SumResourceWithXY {
    x: i32,
    y: i32,
    sum: i32,
}

impl<'r> rocket::response::Responder<'r> for SumResourceWithXY {
    fn respond_to(
        self,
        request: &rocket::Request,
    ) -> Result<rocket::Response<'r>, rocket::http::Status> {
        rocket_contrib::Json(self).respond_to(request)
    }
}

impl From<SumResource> for SumResourceWithXY {
    fn from(source: SumResource) -> SumResourceWithXY {
        SumResourceWithXY {
            x: 0,
            y: 0,
            sum: source.sum,
        }
    }
}

struct SumResourceOnlyString(String);

impl<'r> rocket::response::Responder<'r> for SumResourceOnlyString {
    fn respond_to(
        self,
        request: &rocket::Request,
    ) -> Result<rocket::Response<'r>, rocket::http::Status> {
        self.0.respond_to(request)
    }
}

impl From<SumResourceWithXY> for SumResourceOnlyString {
    fn from(source: SumResourceWithXY) -> SumResourceOnlyString {
        SumResourceOnlyString(source.sum.to_string())
    }
}
