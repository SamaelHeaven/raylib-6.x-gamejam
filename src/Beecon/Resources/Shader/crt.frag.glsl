#version vigilance_300

in vec2 fragTexCoord;
out vec4 finalColor;

uniform sampler2D texture0;
uniform vec2 uResolution;
uniform vec2 uUvScale;
uniform float uWarp;
uniform float uScan;

void main()
{
    vec2 uv = fragTexCoord / uUvScale;
    vec2 dc = abs(0.5 - uv);
    dc *= dc;
    uv.x -= 0.5; uv.x *= 1.0 + (dc.y * (0.3 * uWarp)); uv.x += 0.5;
    uv.y -= 0.5; uv.y *= 1.0 + (dc.x * (0.4 * uWarp)); uv.y += 0.5;
    if (uv.x < 0.0 || uv.x > 1.0 || uv.y < 0.0 || uv.y > 1.0)
    {
        finalColor = vec4(0.0, 0.0, 0.0, 1.0);
        return;
    }

    float apply = abs(sin(uv.y * uResolution.y) * 0.5 * uScan);
    vec3 color = texture(texture0, uv * uUvScale).rgb;
    finalColor = vec4(mix(color, vec3(0.0), apply), 1.0);
}
