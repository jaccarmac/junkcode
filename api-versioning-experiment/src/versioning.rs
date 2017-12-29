extern crate rocket;

enum Version {
    Current,
    SumResourceWithXY,
    NotApplicable,
    SumResourceOnlyString,
}

impl<'i, 'o> From<&'i rocket::Request<'o>> for Version {
    fn from(request: &'i rocket::Request) -> Version {
        match request.headers().get_one("Versioning-Version") {
            Some("withxy") => Version::SumResourceWithXY,
            Some("na") => Version::NotApplicable,
            Some("onlystring") => Version::SumResourceOnlyString,
            _ => Version::Current,
        }
    }
}

pub struct VersionedResourceWrapper<R>(R);

impl<R> VersionedResourceWrapper<R> {
    pub fn new(wrapped: R) -> VersionedResourceWrapper<R> {
        VersionedResourceWrapper(wrapped)
    }
}

impl<'r, R: VersionedResource<'r>> rocket::response::Responder<'r> for VersionedResourceWrapper<R> {
    fn respond_to(
        self,
        request: &rocket::Request,
    ) -> Result<rocket::Response<'r>, rocket::http::Status> {
        match Version::from(request) {
            Version::Current => self.0.respond_to(request),
            Version::SumResourceWithXY => R::SumResourceWithXY::from(self.0).respond_to(request),
            Version::NotApplicable => {
                R::NotApplicable::from(R::SumResourceWithXY::from(self.0)).respond_to(request)
            }
            Version::SumResourceOnlyString => R::SumResourceOnlyString::from(
                R::NotApplicable::from(R::SumResourceWithXY::from(self.0)),
            ).respond_to(request),
        }
    }
}

pub trait VersionedResource<'r>: rocket::response::Responder<'r>
where
    Self: Sized,
{
    type SumResourceWithXY: From<Self> + rocket::response::Responder<'r>;
    type NotApplicable: From<Self::SumResourceWithXY> + rocket::response::Responder<'r>;
    type SumResourceOnlyString: From<Self::NotApplicable> + rocket::response::Responder<'r>;
}
