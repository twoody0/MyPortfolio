@echo off
setlocal

echo Building the project...
call npm run build || goto :error

echo.
echo Uploading to S3...
aws s3 sync dist/ s3://tyler-woody-dev-site --region us-west-2 --delete || goto :error

echo.
echo Invalidating CloudFront cache...
aws cloudfront create-invalidation --distribution-id E3PPAAII3J6HAO --paths "/*" --region us-west-2 || goto :error

echo.
echo Deployment complete!
pause
exit /b

:error
echo ERROR during deployment!
pause
exit /b 1
